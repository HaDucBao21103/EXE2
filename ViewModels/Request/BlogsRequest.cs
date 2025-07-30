namespace ViewModels.Request
{
    public class BlogsRequest
    {
        public string? Id { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public string? ImageUrl { get; set; }
        public Guid? AuthorId { get; set; }
        public string? BlogTypeId { get; set; }
        public string? Status { get; set; }
    }
}
