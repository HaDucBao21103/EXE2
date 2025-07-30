using Core.Base;
using System.Text.Json.Serialization;

namespace BusinessObject
{
    public class RecycleLocations : BaseEntity
    {
        public required string Name { get; set; }
        public required string Address { get; set; }
        public required string ContactNumber { get; set; }
        public required string Description { get; set; }
        public DateTimeOffset OpeningTime { get; set; }
        public DateTimeOffset ClosingTime { get; set; }
        public required string Latitude { get; set; }
        public required string Longitude { get; set; }
        public required string WasteTypeId { get; set; }
        [JsonIgnore]
        public virtual WasteTypes? WasteType { get; set; } = null!;
    }
}
