using System.Text;
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;

namespace GatewayMicroservices.Srvices
{
    public class DestinationService
    {
        public string Uri { get; set; }
        public bool RequiresAuthentication { get; set; }

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
            HttpRequestMessage newRequest = new HttpRequestMessage(new HttpMethod(request.Method), CreateDestinationUri(request));
            newRequest.Content = new StreamContent(request.Body);
            newRequest.Content.Headers.ContentType = new MediaTypeHeaderValue(request.ContentType);


            foreach (var header in request.Headers)
            {
                newRequest.Headers.TryAddWithoutValidation(header.Key, header.Value.ToArray());
            }
            HttpResponseMessage response = await client.SendAsync(newRequest);



            return response;
        }

        private string CreateDestinationUri(HttpRequest request)
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
