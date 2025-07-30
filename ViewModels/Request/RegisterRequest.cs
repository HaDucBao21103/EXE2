using System.ComponentModel.DataAnnotations;

namespace ViewModels.Request
{
    public class RegisterRequest
    {
        [Required]
        public required string FullName { get; set; }
        [Required]
        public required string UserName { get; set; }
        [Required]
        [EmailAddress]
        public required string Email { get; set; }
        [Required]
        public required string Address { get; set; }
        [Required]
        public required string Gender { get; set; }
        public string? Avatar { get; set; }
        [Required]
        [Phone]
        public required string PhoneNumber { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public required DateTimeOffset DateOfBirth { get; set; }
        [Required]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).+$", ErrorMessage = "Mật khẩu phải chứa ít nhất một chữ cái viết hoa, một chữ cái viết thường, một chữ số và một ký tự đặc biệt.")]
        public required string Password { get; set; }
    }
}
