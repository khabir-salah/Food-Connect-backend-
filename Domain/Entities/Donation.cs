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
        public Guid FamilyId { get; set; }
        public Guid OragizationId { get; set; }
        public Guid RecipentId { get; set; }
        public string FoodDetails { get; set; } = default!;
        public int Quantity { get; set; }
        public DateTime ExpirationDate { get; set; }
        public DonationStatus Status { get; set; }
        public DateTime PickUpTime { get; set; }
        public string PickUpLocation { get; set; } = default!;
        public Family Family { get; set; }
        public Recipent Recipent {  get; set; }
        public Organisation Organisation { get; set; }
    }
}
