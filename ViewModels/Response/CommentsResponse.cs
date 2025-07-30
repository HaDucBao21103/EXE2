namespace ViewModels.Response
{
    public class CommentsResponse
    {
        public string? Id { get; set; }
        public string? BlogId { get; set; }
        public Guid? UserId { get; set; }
        public string? Content { get; set; }
        public string? Username { get; set; }
    }
}
