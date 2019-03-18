using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Tools.Ribbon;
using Microsoft.Office.Interop.Word;
using Word = Microsoft.Office.Interop.Word;
//using System.Windows.Forms;
using System.Drawing.Imaging;

namespace BIMTrackWordAddIn
{
    public partial class BIMTrackRibbon
    {
        BIMTrackAPI api;
        RibbonFactory factory;
        private Application application;

        private void BIMTrackRibbon_Load(object sender, RibbonUIEventArgs e)
        {
            api = new BIMTrackAPI();
            factory = Globals.Factory.GetRibbonFactory();
            txtApiKey.Text = api.apiKey;
        }

        private void btnLoadProjects_Click(object sender, RibbonControlEventArgs e)
        {
            string apiKey = txtApiKey.Text;
            api.apiKey = apiKey;
            api.hubId = api.getHubs().First().Id;
            var projects = api.getProjects(api.hubId);
 
            foreach (Project p  in projects)
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
            int projectId = api.projects.Find(x => x.Name == drpDwnProjects.SelectedItem.Label).Id;

            var createdIssue = api.createIssue(selectionText, api.hubId, projectId);

            foreach (System.Drawing.Image image in images)
            {
                var viewpoint = api.createViewpoint(api.hubId, projectId, createdIssue.Id, image);
            }

            // https://vincentcadoret.bimtrackapp.co/Projects/7991/Issues/10
            // Convert selected text to link to newly created issue.
            //  TODO: link creation is broken since I added the image.
            Object hyperlink = "https://vincentcadoret.bimtrackapp.co/Projects/" + createdIssue.ProjectId + "/Issues/" + createdIssue.Number;
            Object oRange = selection.Range;
            selection.Hyperlinks.Add(oRange, ref hyperlink);
        }
    }
}
