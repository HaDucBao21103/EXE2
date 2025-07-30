namespace ViewModels.Request
{
    public class BlogTypesRequest
    {
        public string? Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
    }
}
