namespace ViewModels.Response
{
    public class TransactionResponse
    {
        public Guid Id { get; set; }
        public float Amount { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string PaymentMethod { get; set; }
        public Guid UserId { get; set; }
        public string TransactionTypeId { get; set; }
        public string? CampaignId { get; set; }
        public DateTimeOffset CreatedTime { get; set; }

        // Optional: enrich thêm thông tin
        public string? CampaignTitle { get; set; }
        public string? TransactionTypeName { get; set; }
    }
}
