using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Tools.Ribbon;
using Microsoft.Office.Interop.Word;
using Word = Microsoft.Office.Interop.Word;
//using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Windows.Forms;
using WordAddIn1.ApiObjects;

namespace BIMTrackWordAddIn
{
    public partial class BIMTrackRibbon
    {
        BIMTrackAPI api;
        RibbonFactory factory;
        private Word.Application application;

        private void BIMTrackRibbon_Load(object sender, RibbonUIEventArgs e)
        {
            api = new BIMTrackAPI();
            factory = Globals.Factory.GetRibbonFactory();
            txtApiKey.Text = api.ApiKey;
        }

        private void btnLoadProjects_Click(object sender, RibbonControlEventArgs e)
        {
            string apiKey = txtApiKey.Text;
            api.ApiKey = apiKey;
            api.LoadHubs();
            var hub = api.Hubs.FirstOrDefault();
            if (hub == null)
            {
                return;
            }

            api.HubId = hub.Id;
            api.HubName = hub.Name;
            api.LoadProjects();
 
            foreach (Project p  in api.Projects)
            {       
                var item = factory.CreateRibbonDropDownItem();

                item.Label = p.Name;
                drpDwnProjects.Items.Add(item);
            }
        }

        private void btnCreateIssue_Click(object sender, RibbonControlEventArgs e)
        {
            application = Globals.ThisAddIn.Application;

            Word.Selection selection = application.Selection;
            string selectionText = selection.Text; // TODO: we may need to clean things up, or iterate through each line as an issue?

            if (String.IsNullOrWhiteSpace(selectionText))
            {
                MessageBox.Show("Text is required to create an issue.");
                return;
            }

            selectionText = System.Text.RegularExpressions.Regex.Replace(selectionText, @"\t|\n|\r", "");
            while (selectionText.EndsWith("/"))
            {
                selectionText = selectionText.Remove(selectionText.Length - 1);
            }

            InlineShapes inlineShapes = selection.InlineShapes;

            List<System.Drawing.Image> images = new List<System.Drawing.Image>();
            foreach (InlineShape inlineShape in inlineShapes)
            {
                inlineShape.Select(); // TODO: this resets the selection, which prevents multiple image uploads.
                selection.Copy();
                if (System.Windows.Forms.Clipboard.GetDataObject() != null)
                {
                    System.Windows.Forms.IDataObject data = System.Windows.Forms.Clipboard.GetDataObject();

                    if (data.GetDataPresent(System.Windows.Forms.DataFormats.Bitmap))
                    {
                        System.Drawing.Image image = (System.Drawing.Image)data.GetData(System.Windows.Forms.DataFormats.Bitmap, true);
                        images.Add(image);
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("The Data In Clipboard is not as image format");
                    }
                }
                else
                {
                    // MessageBox.Show("The Clipboard was empty");
                }
            }

            // get currently selected project id
            int projectId = api.Projects.Find(x => x.Name == drpDwnProjects.SelectedItem.Label).Id;

            var createdIssue = api.CreateIssue(selectionText, api.HubId, projectId);

            foreach (System.Drawing.Image image in images)
            {
                api.CreateViewpoint(api.HubId, projectId, createdIssue.Id, image);
                image.Dispose();
            }

            
            // https://[hubName].bimtrackapp.co/Projects/{projectId}/Issues/{issueNumber}
            // Convert selected text to link to newly created issue.
            //  TODO: link creation is broken since I added the image.
            Object hyperlink = $"https://{api.HubName}.bimtrackapp.co/Projects/" + createdIssue.ProjectId + "/Issues/" + createdIssue.Number;
            Object oRange = selection.Range;
            selection.Hyperlinks.Add(oRange, ref hyperlink);
        }
    }
}
