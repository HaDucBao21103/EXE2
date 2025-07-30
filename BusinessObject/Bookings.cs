using Core.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObject
{
    public class Bookings : BaseEntity
    {
        public string Location { get; set; } // Địa điểm muốn tuyên truyền (ví dụ: trường học, khu phố)

        public DateTime BookingDate { get; set; } // Ngày diễn ra hoạt động tuyên truyền

        public string? Description { get; set; } // Mô tả chi tiết về buổi tuyên truyền

        public string ContactPersonName { get; set; } // Tên người liên hệ tại địa điểm

        public string ContactPersonPhone { get; set; } // Số điện thoại người liên hệ

        public string? ContactPersonEmail { get; set; } // Email người liên hệ

        public string Status { get; set; } // Trạng thái của booking (e.g., Pending, Approved, Rejected, Completed)

        // Khóa ngoại tới Users
        public Guid UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual Users? User { get; set; } // Navigation property để truy cập thông tin User

    }
}
