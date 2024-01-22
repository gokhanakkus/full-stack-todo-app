using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Todo.Shared.Data;

namespace Todo.Data.Models
{
    public abstract class BaseEntity : IAuditableEntity<long>, ICanDisable
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual long Id { get; set; }
        public bool IsDisabled { get; set; } = false;
        public long CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public long LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
    }
}
