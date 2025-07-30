using Core.Base;

namespace BusinessObject
{
    public class Brands : BaseEntity
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public string? LogoUrl { get; set; }
        public string? WebsiteUrl { get; set; }
    }
}
