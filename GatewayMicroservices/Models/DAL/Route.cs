using GatewayMicroservices.Models.DAL.Base;

namespace GatewayMicroservices.Models.DAL
{
    public class Route : AEntityWithOneKey
    {
        public string Endpoint { get; set; }
        public string DestinationUri { get; set; }
        public bool RequiresAuthentication { get; set; }
        public string ReplaceMode { get; set; }
    }
}
