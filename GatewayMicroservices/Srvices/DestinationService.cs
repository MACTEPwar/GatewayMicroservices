using System.Text;

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
            string requestContent;
            using (Stream receiveStream = request.Body)
            {
                using (StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8))
                {
                    requestContent = readStream.ReadToEnd();
                }
            }

            HttpClient client = new HttpClient();
            HttpRequestMessage newRequest = new HttpRequestMessage(new HttpMethod(request.Method), CreateDestinationUri(request));
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
                endpoint = endpointSplit[1];

            return Uri + endpoint + queryString;
        }
    }
}
