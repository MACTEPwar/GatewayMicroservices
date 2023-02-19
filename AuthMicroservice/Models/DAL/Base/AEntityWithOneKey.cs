using System.ComponentModel.DataAnnotations;

namespace AuthMicroservice.Models.DAL.Base
{
    /// <summary>
    /// Base abstract class for data base models
    /// </summary>
    public abstract class AEntityWithOneKey : IEntityWithOneKey
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid? CreateUser { get; set; } = Guid.NewGuid();
        public Guid? LastUpdateUser { get; set; }
        public DateTime? InsertedDate { get; set; }
        public DateTime? LastModifyDate { get; set; }
        public bool IsDeleted { get; set; } = false;

    }

    /// <summary>
    /// Base interface for data base models
    /// </summary>
    public interface IEntityWithOneKey
    {
        public Guid Id { get; set; }
        public Guid? CreateUser { get; set; }
        public Guid? LastUpdateUser { get; set; }
        public DateTime? InsertedDate { get; set; }
        public DateTime? LastModifyDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
