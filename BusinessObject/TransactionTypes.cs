using Core.Base;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BusinessObject
{
    public class TransactionTypes : BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string NormalizedName { get; set; } = string.Empty;

        [MaxLength(255)]
        public string Description { get; set; } = string.Empty;

        // IN: tiền vào, OUT: tiền ra
        [MaxLength(20)]
        public string Category { get; set; } = "IN"; // Default IN

        [JsonIgnore]
        public virtual ICollection<TransactionLogs>? TransactionLogs { get; set; }

        public TransactionTypes() { }

        public TransactionTypes(string id, string name, string description, string category = "IN")
        {
            Id = id;
            Name = name;
            NormalizedName = name.ToUpper();
            Description = description;
            Category = category;
            CreatedBy = "System";
            CreatedTime = DateTimeOffset.UtcNow;
        }
    }
}
