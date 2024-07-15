using Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class PickUp
    {
        public Guid Id { get; set; }
        public Guid DonationId { get; set; }
        public Guid RecipentId { get; set; }
        public DateTime PickUpTime { get; set; }
        public string PickUpLocation { get; set; } = default!;
        public Status PickUpStatus { get; set; }


    }
}
