using System.Text;
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http.Headers;
using GatewayMicroservices.Models.DAL;
using System.Net;

namespace GatewayMicroservices.Srvices
{
    public class DestinationService
    {
        public string Uri { get; set; }
        public bool RequiresAuthentication { get; set; }

        public string ReplaceMode { get; set; }

        public DestinationService() { }

        //public DestinationService(string uri, bool requiresAuthentication)
        //{
        //    Uri = uri;
        //    RequiresAuthentication = requiresAuthentication;
        //}

        //public DestinationService(string uri)
        //    : this(uri, false)
        //{
        //}

        //private DestinationService()
        //{
        //    Uri = "/";
        //    RequiresAuthentication = false;
        //}

        public async Task<HttpResponseMessage> SendRequest(HttpRequest request)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders
              .Accept
              .Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpRequestMessage newRequest = new HttpRequestMessage(new HttpMethod(request.Method), CreateDestinationUri(request));
            if (request.ContentLength > 0)
            {
                newRequest.Content = new StreamContent(request.Body);
                newRequest.Content.Headers.ContentType = new MediaTypeHeaderValue(request.ContentType);
            }
            


            foreach (var header in request.Headers)
            {
                if (header.Key != "Content-Type" && header.Key != "Content-Length")
                {
                    var test = header.Value.ToString();
                    newRequest.Headers.Add(header.Key, header.Value.ToString());
                }

            }

            HttpResponseMessage response = await client.SendAsync(newRequest);

            return response;
        }

        private string CreateDestinationUri(HttpRequest request)
        {
            switch (ReplaceMode){
                case "GraphQl":
                    {
                        return Uri + "graphql";
                    }
                default :
                    {
                        string requestPath = request.Path.ToString();
                        string queryString = request.QueryString.ToString();

                        string endpoint = "";
                        string[] endpointSplit = requestPath.Substring(1).Split('/');

                        if (endpointSplit.Length > 1)
                            endpoint = $"{endpointSplit[0]}/{endpointSplit[1]}/{endpointSplit[2]}";

                        return Uri + endpoint + queryString;
                    }
            }
            
        }
    }
}
