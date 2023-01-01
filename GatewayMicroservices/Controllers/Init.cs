using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GatewayMicroservices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Init : ControllerBase
    {
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
