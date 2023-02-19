using AuthMicroservice.Models.DAL.Base;

namespace AuthMicroservice.Models.DAL
{
    public class Scope : AEntityWithOneKey
    {
        public string Name { get; set; }
        public Scope ParentScope { get; set; }
        public Guid ParentScopeId { get; set; }

        public virtual ICollection<User> Users{ get; set; }
    }
}
