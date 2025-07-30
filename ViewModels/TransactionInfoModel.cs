namespace ViewModels
{
    public class TransactionInfoModel
    {
        public float Amount { get; set; }
        public string Description { get; set; }
        public Guid UserId { get; set; } // Người thực hiện giao dịch
        public string TransactionTypeId { get; set; } // "DONATE_CAMPAIGN", "DONATE_SINGLE", etc.
        public string CampaignId { get; set; }
    }

}
