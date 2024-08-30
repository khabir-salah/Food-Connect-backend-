
namespace Domain.Entities
{
    public class User : Auditables
    {
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string? PasswordResetToken { get; set; } 
        public bool IsEmailConfirmed { get; set; } = false;
        public DateTime? PasswordExpireTime { get; set; }
        public string RoleId { get; set; } = null!;
        public string? ProfileImage { get; set; }
        public string PhoneNumber { get; set; } = default!;
        public string? Address { get; set; }
        public string? Name { get; set; }
        public bool IsActivated { get; set; } 
        public virtual Role Role { get; set; }
        public ICollection<Donation> Donations { get; set; }    

    }
}
