using Core.Base;
using System.Text.Json.Serialization;

namespace BusinessObject
{
    public class WasteTypes : BaseEntity
    {
        public required string TypeName { get; set; }
        public required string Description { get; set; }
        public string? IconUrl { get; set; }
        [JsonIgnore]
        public virtual ICollection<Wastes>? Wastes { get; set; }
        [JsonIgnore]
        public virtual ICollection<RecycleLocations>? RecycleLocations { get; set; }
    }
}

