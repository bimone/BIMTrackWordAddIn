using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;
using RestSharp;
using WordAddIn1.ApiObjects;
using Image = System.Drawing.Image;

namespace BIMTrackWordAddIn
{
    internal class BIMTrackAPI
    {
        public string ApiKey { get; set; }
        public int HubId { get; set; }
        public string HubName { get; set; }
        public List<Hub> Hubs { get; set; } = new List<Hub>();
        public List<Project> Projects { get; set; } = new List<Project>();

        public void LoadHubs()
        {
            Hubs.Clear();
            Projects.Clear();
            // Set the base url for the API.
            var client = new RestClient("https://api.bimtrackapp.co/v2/");

            var request = new RestRequest("hubs", Method.GET);

            // easily add HTTP Headers
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Authorization", "Bearer " + ApiKey);

            // or automatically deserialize result
            // return content type is sniffed but can be explicitly set via RestClient.AddHandler();
            var response = client.Execute<List<Hub>>(request);

            if (!response.IsSuccessful)
            {
                MessageBox.Show(response.ErrorMessage?? response.ErrorException?.Message ?? response.StatusCode.ToString());
                throw new WebException(response.StatusCode.ToString());
            }


            Hubs = response.Data;         
        }

        public void LoadProjects()
        {
            // Set the base url for the API.
            var client = new RestClient("https://api.bimtrackapp.co/v2/");

            var request = new RestRequest("hubs/" + HubId + "/projects", Method.GET);

            // easily add HTTP Headers
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Authorization", "Bearer " + ApiKey);

            // or automatically deserialize result
            // return content type is sniffed but can be explicitly set via RestClient.AddHandler();
            var response = client.Execute<List<Project>>(request);
            if (!response.IsSuccessful)
            {
                MessageBox.Show(response.ErrorMessage ?? response.ErrorException?.Message ?? response.StatusCode.ToString());
                throw new WebException(response.StatusCode.ToString());
            }

            Projects = response.Data;
        }

        public Issue CreateIssue(string issueTitle, int hubId, int projectId)
        {
            // Set the base url for the API.
            var client = new RestClient("https://api.bimtrackapp.co/v2/");

            var jsonToSend = "{" +
                             "\"Title\": \"" + issueTitle + "\"" +
                             "}";

            var request = new RestRequest("hubs/" + hubId + "/projects/" + projectId + "/issues", Method.POST);
            request.AddParameter("application/json", jsonToSend, ParameterType.RequestBody);
            request.RequestFormat = DataFormat.Json;

            // easily add HTTP Headers
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Authorization", "Bearer " + ApiKey);

            // or automatically deserialize result
            // return content type is sniffed but can be explicitly set via RestClient.AddHandler();
            var response = client.Execute<Issue>(request);
            if (!response.IsSuccessful)
            {
                MessageBox.Show(response.ErrorMessage ?? response.ErrorException?.Message ?? response.StatusCode.ToString());
                throw new WebException(response.StatusCode.ToString());
            }
            return response.Data;
        }

        public Viewpoint CreateViewpoint(int hubId, int projectId, int issueId, Image image)
        {
            var client = new RestClient("https://api.bimtrackapp.co/v2/");

            var jsonToSend = "{" +
                             "\"ViewType\": \"None\"" +
                             "}";

            var request = new RestRequest($"hubs/{hubId}/projects/{projectId}/issues/{issueId}/viewpoints", Method.POST);

            request.RequestFormat = DataFormat.Json;

            // easily add HTTP Headers
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Authorization", "Bearer " + ApiKey);

            request.AddFile("image", Encoding.UTF32.GetBytes(jsonToSend), "test_image.json", "application/json");
            request.AddFile("image", imageToByteArray(image), "test_image.png", "application/png");

            var response = client.Execute<Viewpoint>(request);

            return response.Data;
        }

        public byte[] imageToByteArray(Image imageIn)
        {
            var ms = new MemoryStream();
            imageIn.Save(ms, ImageFormat.Png);
            return ms.ToArray();
        }
        }
}