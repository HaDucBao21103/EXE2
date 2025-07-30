namespace ViewModels.Request
{
    public class TransactionCreateRequest
    {
        public float Amount { get; set; }
        public string Description { get; set; }
        public Guid UserId { get; set; }
        public string TransactionTypeId { get; set; }
        public String? CampaignId { get; set; }
    }
}
