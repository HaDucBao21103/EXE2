using Core.Utils;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using System.Text.Json.Serialization;

namespace BusinessObject
{
    public class Users : IdentityUser<Guid>
    {
        public required string FullName { get; set; }
        public required string Address { get; set; }
        public required string Gender { get; set; }
        public string? Avatar { get; set; }
        public DateTimeOffset DateOfBirth { get; set; }
        public DateTimeOffset JoinedDate { get; set; }
        public string? CreatedBy { get; set; }
        public string? LastUpdatedBy { get; set; }
        public string? DeletedBy { get; set; }
        public DateTimeOffset? CreatedTime { get; set; }
        public DateTimeOffset? LastUpdatedTime { get; set; }
        public DateTimeOffset? DeletedTime { get; set; }

        public Guid RoleId { get; set; }
        [ForeignKey(nameof(RoleId))]
        public virtual Roles? Roles { get; set; }

        [JsonIgnore]
        public virtual ICollection<Blogs>? Blogs { get; set; }
        [JsonIgnore]
        public virtual ICollection<Comments>? Comments { get; set; }
        [JsonIgnore]
        public virtual ICollection<Bookings>? Bookings { get; set; }
        [DefaultValue(false)]
        public bool IsDeleted { get; set; }

        public void SetAuditFields(ClaimsPrincipal user)
        {
            string? username = user.FindFirst(ClaimTypes.Name)?.Value;

            if (CreatedBy == null)
            {
                CreatedBy = username;
                CreatedTime = CoreHelper.SystemTimeNow;
            }

            LastUpdatedBy = username;
            LastUpdatedTime = CoreHelper.SystemTimeNow;
        }

        public void SetDeletedFields(ClaimsPrincipal user)
        {
            DeletedBy = user.FindFirst(ClaimTypes.Name)?.Value;
            DeletedTime = CoreHelper.SystemTimeNow;
        }
    }
}
