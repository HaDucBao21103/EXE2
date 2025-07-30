using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObject
{
    public class RefreshToken
    {
        [Key]
        public string Id { get; set; }
        public string Token { get; set; }
        public string JwtId { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset ExpiryDate { get; set; }
        public bool Used { get; set; }
        public bool Revoked { get; set; }

        public Guid UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual Users? User { get; set; }

        public RefreshToken()
        {
            Id = Guid.NewGuid().ToString("N");
        }
    }
}
