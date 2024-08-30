

namespace Domain.Entities
{
    public class Message : Auditables
    {
        public Guid DonorId { get; set; }
        public Guid RecipientId { get; set; }
        public Guid DonationId { get; set; }
        public string? Content { get; set; }
        public DateTime SentAt { get; set; } = DateTime.Now;
        public bool IsRead { get; set; } 
    }
}
