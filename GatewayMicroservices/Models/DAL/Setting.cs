using System.ComponentModel.DataAnnotations;

namespace GatewayMicroservices.Models.DAL
{
    public class Setting
    {
        [Required]
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
