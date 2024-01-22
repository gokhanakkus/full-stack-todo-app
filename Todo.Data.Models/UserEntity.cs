using Microsoft.EntityFrameworkCore;

namespace Todo.Data.Models
{
    [Index(nameof(Username), IsUnique = true, Name = "IX_UNIQUE_Username")]
    [Index(nameof(Email), IsUnique = true, Name = "IX_UNIQUE_Email")]
    public class UserEntity : BaseEntity
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; } = string.Empty;
    }
}
