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
        public Guid? Recipient { get; set; }
        public User User { get; set; }  
        public string FoodDetails { get; set; } = default!;
        public int Quantity { get; set; }
        public DateTime ExpirationDate { get; set; }
        public DonationStatus Status { get; set; }
        public DateTime PickUpTime { get; set; }
        public DateTime? LastClaimTime { get; set; }
        public string PickUpLocation { get; set; } = default!;          
        public string Images { get; set; } = default!;
        public string PrimaryImageUrl { get; set; } =  default!;
        public DonationMadeBy DonationMadeBy { get; set; }
        public string? ReasonForDisapproval { get; set; }
        public Guid? ManagerId { get; set; }
        public Manager Manager { get; set; }
    }
}
