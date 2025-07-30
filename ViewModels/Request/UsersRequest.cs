namespace ViewModels.Request
{
    public class UsersRequest
    {
        public Guid Id { get; set; }
        public string? UserName { get; set; }
        public string? NormalizedUserName { get; set; }
        public string? Email { get; set; }
        public string? NormalizedEmail { get; set; }
        public string? PasswordHash { get; set; }
        public bool? EmailConfirmed { get; set; }
        public string? PhoneNumber { get; set; }
        public string? FullName { get; set; }
        public string? Address { get; set; }
        public string? Gender { get; set; }
        public string? Avatar { get; set; }
    }

    public class UsersRequestRole
    {
        public Guid UserId { get; set; }
        public Guid? RoleId { get; set; }
    }
}
