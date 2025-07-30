using Core.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObject
{
    public class Campaigns : BaseEntity
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public Guid UserId { get; set; }
        public decimal? TargetAmount { get; set; }
        public decimal? RaisedAmount { get; set; }
        public string? Status { get; set; }
        public DateTimeOffset? StartDate { get; set; }
        public DateTimeOffset? EndDate { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual Users? User { get; set; }
    }
}
