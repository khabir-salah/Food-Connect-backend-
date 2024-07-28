
namespace Domain.Entities
{
    public class User : Auditables
    {
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string? PasswordResetToken { get; set; } 
        public bool IsEmailConfirmed { get; set; } = false;
        public DateTime? PasswordExpireTime { get; set; }
        public Guid RoleId { get; set; }
        public string? ProfileImage { get; set; }
        public string PhoneNumber { get; set; } = default!;
        public string? Address { get; set; }
        public string? Name { get; set; }
        public bool IsActivated { get; set; } = true;
        public virtual Role Role { get; set; }
        public virtual Donation Donation { get; set; }
        public virtual Family Family { get; set; }
        public virtual Individual Recipent { get; set; }
        public virtual Organisation Organisation { get; set; }
    }
}
