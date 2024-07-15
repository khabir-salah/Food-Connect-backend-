namespace Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string ConfirmPassword { get; set; } = default!;
        public Guid RoleId { get; set; }
        public virtual Role Role { get; set; }
        public virtual Donor Donor { get; set; }
        public virtual Recipent Recipent { get; set; }

    }
}
