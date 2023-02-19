using AuthMicroservice.Models;
using AuthMicroservice.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;

namespace AuthMicroservice.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        public readonly AuthService _authService;
        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpGet("Test")]
        public IActionResult GetTest([FromQuery] TokenRequest tokenRequest)
        {
            return Ok(tokenRequest);
        }

        [HttpPost("Token")]
        public IActionResult GetToken([FromBody] TokenRequest tokenRequest)
        {
            var identity = _authService.GetIdentity(tokenRequest.Login, tokenRequest.Password);
            if (identity == null)
            {
                return new ContentResult() { Content = "Invalid username or password", StatusCode = (int?)HttpStatusCode.Unauthorized };
            }

            var now = DateTime.UtcNow;

            var jwt = new JwtSecurityToken(
                   issuer: AuthOptions.ISSUER,
                   audience: AuthOptions.AUDIENCE,
                   notBefore: now,
                   claims: identity.Claims,
                   expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                   signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return new OkObjectResult(new TokenResponse()
            {
                AccessToken = encodedJwt,
                UserName = tokenRequest.Login,
            });
        }
    }
}
