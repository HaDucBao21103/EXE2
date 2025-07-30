using Core.Base;
using System.Text.Json.Serialization;

namespace BusinessObject
{
    public class BlogTypes : BaseEntity
    {
        public required string Name { get; set; }
        public required string Description { get; set; }

        [JsonIgnore]
        public virtual ICollection<Blogs>? Blogs { get; set; }
    }
}
