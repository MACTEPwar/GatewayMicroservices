namespace AuthMicroservice.Models
{
    public class TokenResponse
    {
        public string AccessToken { get; set; }
        public string AccessTokenSec { get; set; }
        public string UserName { get; set; }
    }
}
