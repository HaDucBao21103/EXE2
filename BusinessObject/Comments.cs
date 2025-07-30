using Core.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObject
{
    public class Comments : BaseEntity
    {
        public required string BlogId { get; set; }
        public required Guid UserId { get; set; }
        public required string Content { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual Users? User { get; set; }
        [ForeignKey(nameof(BlogId))]
        public virtual Blogs? Blog { get; set; }
    }
}
