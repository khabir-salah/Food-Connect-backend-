

namespace Domain.Entities
{
    public class FoodCollection : Auditables
    {
        public Guid Recipient { get; set; }
        public Guid UserId { get; set; }
        public Guid DonationId { get; set; }
        public string? Review { get; set; }
        public string[]? ReviewImages { get; set; }
        public Donation Donation { get; set; }
    }
}
