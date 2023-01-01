using GatewayMicroservices.Models.DAL.Base;
using System.ComponentModel.DataAnnotations;

namespace GatewayMicroservices.Models.DAL
{
    public class Setting : AEntityWithOneKey
    {
        [Required]
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
