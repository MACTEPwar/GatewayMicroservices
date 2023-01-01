namespace GatewayMicroservices.Models.DAL
{
    public class Route
    {
        public string Endpoint { get; set; }
        public string DestinationUri { get; set; }
        public bool RequiresAuthentication { get; set; }
    }
}
