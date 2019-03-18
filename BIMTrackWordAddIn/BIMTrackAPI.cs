using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace BIMTrackWordAddIn
{
    public class Hub
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class Issue
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string Title { get; set; }
        public int Number { get; set; }
        public string Description { get; set; }
        public string Group { get; set; }
        public int AssignedToUserId { get; set; }
        public string LastModificationDate { get; set; }
    }

    public class Viewpoint
    {
        public int Id { get; set; }
        public int IssueId { get; set; }
        public string ViewType { get; set; }
        public string Source { get; set; }
        public string ViewName { get; set; }
        public string ModelName { get; set; }
        public Image Image { get; set; }
    }

    public class Image
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Size { get; set; }
        public string ThumbnailUrl { get; set; }
        public DateTime ThumbnailUrlExpiration { get; set; }
        public DateTime Date { get; set; }
        public string Url { get; set; }
        public DateTime UrlExpiration { get; set; }
    }

    class BIMTrackAPI
    {
        public string apiKey = "";
        public int hubId;
        public List<Hub> hubs;
        public List<Project> projects;

        public List<Hub> getHubs()
        {
            // Set the base url for the API.
            var client = new RestClient("https://api.bimtrackapp.co/v2/");

            var request = new RestRequest("hubs", Method.GET);

            // easily add HTTP Headers
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Authorization", "Bearer "+apiKey);

            // or automatically deserialize result
            // return content type is sniffed but can be explicitly set via RestClient.AddHandler();
            var response = client.Execute<List<Hub>>(request);
            hubs = response.Data;

            return response.Data;
        }

        public List<Project> getProjects(int hubId)
        {
            // Set the base url for the API.
            var client = new RestClient("https://api.bimtrackapp.co/v2/");

            var request = new RestRequest("hubs/"+hubId.ToString()+"/projects", Method.GET);

            // easily add HTTP Headers
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Authorization", "Bearer " + apiKey);

            // or automatically deserialize result
            // return content type is sniffed but can be explicitly set via RestClient.AddHandler();
            var response = client.Execute<List<Project>>(request);
            projects = response.Data;

            return response.Data;
        }

        public Issue createIssue(string issueTitle, int hubId, int projectId)
        {
            // Set the base url for the API.
            var client = new RestClient("https://api.bimtrackapp.co/v2/");

            string jsonToSend = "{" +
                "\"Title\": \"" + issueTitle + "\""+
                "}";

            var request = new RestRequest("hubs/" + hubId.ToString() + "/projects/" + projectId.ToString() + "/issues", Method.POST);
            request.AddParameter("application/json", jsonToSend, ParameterType.RequestBody);
            request.RequestFormat = DataFormat.Json;

            // easily add HTTP Headers
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Authorization", "Bearer " + apiKey);

            // or automatically deserialize result
            // return content type is sniffed but can be explicitly set via RestClient.AddHandler();
            var response = client.Execute<Issue>(request);
            
            return response.Data;
        }

        public Viewpoint createViewpoint(int hubId, int projectId, int issueId, System.Drawing.Image image)
        {
            var client = new RestClient("https://api.bimtrackapp.co/v2/");

            string jsonToSend = "{" +
                "\"ViewType\": \"None\"" +
                "}";

            var request = new RestRequest("hubs/" + hubId.ToString() + "/projects/" + projectId.ToString() + "/issues/" 
                + issueId.ToString() + "/viewpoints", Method.POST);
            
            request.RequestFormat = DataFormat.Json;

            // easily add HTTP Headers
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Authorization", "Bearer " + apiKey);

            request.AddFile("image", Encoding.UTF32.GetBytes(jsonToSend), "test_image.json", "application/json");
            request.AddFile("image", imageToByteArray(image), "test_image.png", "application/png");

            var response = client.Execute<Viewpoint>(request);

            return response.Data;
        }

        public byte[] imageToByteArray(System.Drawing.Image imageIn)
        {
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            return ms.ToArray();
        }
    }
}
