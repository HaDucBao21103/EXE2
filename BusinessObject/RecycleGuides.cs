using Core.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObject
{
    public class RecycleGuides : BaseEntity
    {
        public required string Title { get; set; }
        public required string Content { get; set; }
        public string? ImageUrl { get; set; }
        public string? VideoUrl { get; set; }
        public required string WasteId { get; set; }
        [ForeignKey(nameof(WasteId))]
        public virtual Wastes? Waste { get; set; }
    }
}
