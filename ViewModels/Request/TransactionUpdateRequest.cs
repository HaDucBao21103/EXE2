namespace ViewModels.Request
{
    public class TransactionUpdateRequest
    {
        public Guid Id { get; set; }
        public string Status { get; set; } = "Pending";     // Success, Failed, etc.
        public string PaymentMethod { get; set; } = "Momo"; // Momo, ZaloPay, etc.
    }
}
