using Core.Utils;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace Core.Base
{
    public abstract class BaseEntity
    {
        [Key]
        public string Id { get; set; }
        public string? CreatedBy { get; set; }
        public string? LastUpdatedBy { get; set; }
        public string? DeletedBy { get; set; }
        public DateTimeOffset? CreatedTime { get; set; }
        public DateTimeOffset? LastUpdatedTime { get; set; }
        public DateTimeOffset? DeletedTime { get; set; }
        [DefaultValue(false)]
        public bool IsDeleted { get; set; }
        protected BaseEntity()
        {
            Id = Guid.NewGuid().ToString("N");
            CreatedTime = LastUpdatedTime = CoreHelper.SystemTimeNow;
        }

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
