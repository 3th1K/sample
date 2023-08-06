namespace UserService.Api.Models
{
    public class UserResponse
    {
        public string UserId { get; set; } = null!;

        public string Username { get; set; } = null!;

        public string Email { get; set; } = null!;

        public DateTime DateOfBirth { get; set; }

        public string Country { get; set; } = null!;

        public string State { get; set; } = null!;

        public string City { get; set; } = null!;

        public string Gender { get; set; } = null!;

        public string? Firstname { get; set; }

        public string? Lastname { get; set; }
    }
}
