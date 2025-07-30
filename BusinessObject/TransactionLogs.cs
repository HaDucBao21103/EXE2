using Core.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObject
{
    public class TransactionLogs : BaseEntity
    {

        [Range(0, double.MaxValue)]
        public float Amount { get; set; }

        [MaxLength(255)]
        public string Description { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? MomoOrderId { get; set; }

        [MaxLength(50)]
        public string Status { get; set; } = string.Empty; // Success, Failed, Pending, etc.

        [MaxLength(50)]
        public string PaymentMethod { get; set; } = string.Empty; // Momo, ZaloPay, etc.

        // Người thực hiện giao dịch (người donate)
        public Guid UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public virtual Users Users { get; set; }

        // Loại giao dịch
        public string TransactionTypeId { get; set; } = string.Empty;
        [ForeignKey(nameof(TransactionTypeId))]
        public virtual TransactionTypes TransactionTypes { get; set; }

        // Chiến dịch được donate (nullable nếu là donate đơn lẻ)
        public string? CampaignId { get; set; }
        [ForeignKey(nameof(CampaignId))]
        public virtual Campaigns? Campaign { get; set; }
    }
}
