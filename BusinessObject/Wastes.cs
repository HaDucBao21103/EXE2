using Core.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObject
{
    public class Wastes : BaseEntity
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public string? ImageUrl { get; set; }
        public required string WasteTypeId { get; set; }
        [ForeignKey(nameof(WasteTypeId))]
        public virtual WasteTypes? WasteType { get; set; }
    }
}
