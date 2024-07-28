using Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Donation : Auditables
    {
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
        public string FoodDetails { get; set; } = default!;
        public int Quantity { get; set; }
        public DateTime ExpirationDate { get; set; }
        public DonationStatus Status { get; set; }
        public DateTime PickUpTime { get; set; }
        public string PickUpLocation { get; set; } = default!;
        public List<string> Images { get; set; } = default!;
        public string PrimaryImageUrl { get; set; } =  default!;
        public DonationMadeBy DonationMadeBy { get; set; }
    }
}
