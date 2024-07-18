namespace Domain.Entities
{
    public class User : Auditables
    {
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
        public Guid RoleId { get; set; }
        public virtual Role Role { get; set; }
        public virtual Family Family { get; set; }
        public virtual Recipent Recipent { get; set; }
        public virtual Organisation Organisation { get; set; }
    }
}
