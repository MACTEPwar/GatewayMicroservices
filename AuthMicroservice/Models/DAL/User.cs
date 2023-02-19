using AuthMicroservice.Models.DAL.Base;

namespace AuthMicroservice.Models.DAL
{
    public class User : AEntityWithOneKey
    {
        public string Login { get; set; }
        public string Password { get; set; }

        public virtual ICollection<Scope> Scopes{ get; set; }
    }
}
