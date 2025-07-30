using Core.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObject
{
    public class Post : BaseEntity
    {
        public required string Title { get; set; }
        [DataType(DataType.Text)]
        public required string Content { get; set; }
        public string? ImageUrl { get; set; }
        public Guid AuthorId { get; set; }
        public string? Status { get; set; }

        [ForeignKey(nameof(AuthorId))]
        public virtual Users? Author { get; set; }

    }
}
