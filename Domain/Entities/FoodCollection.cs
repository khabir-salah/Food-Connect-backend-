using Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class FoodCollection
    {
        public Guid Id { get; set; }
        public Guid FamilyId { get; set; }
        public Guid OragizationId { get; set; }
        public Guid RecipentId { get; set; }
        public Donation Donation { get; set; }
        public Guid DonationId { get; set; }
        public string? Review { get; set; }
        public string[]? ReviewImages { get; set; }
        public Family Family { get; set; }
        public Recipent Recipent { get; set; }
        public Organisation Organisation { get; set; }
        public DonationStatus Status { get; set; } = DonationStatus.Received;

    }
}
