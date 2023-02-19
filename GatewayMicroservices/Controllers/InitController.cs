using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GatewayMicroservices.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InitController : ControllerBase
    {
        [HttpGet(Name = "InitRoutes")]
        public bool InitRoutes()
        {
            try
            {
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
