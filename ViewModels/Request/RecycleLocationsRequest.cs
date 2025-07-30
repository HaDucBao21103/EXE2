namespace ViewModels.Request
{
    public class RecycleLocationsRequest
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? ContactNumber { get; set; }
        public string? Description { get; set; }
        public DateTimeOffset OpeningTime { get; set; }
        public DateTimeOffset ClosingTime { get; set; }
        public string? Latitude { get; set; }
        public string? Longitude { get; set; }
        public string? WasteTypeId { get; set; }
    }
}
