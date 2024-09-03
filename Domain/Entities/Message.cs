

namespace Domain.Entities
{
    public class Message : Auditables
    {
        public Guid DonationId { get; set; }
        public Guid UserId { get; set; }
        public Guid RecipientId { get; set; }
        public Guid DonorId { get; set; }
        public string? Content { get; set; }
        public DateTime SentAt { get; set; } = DateTime.Now;
        
    }
}
