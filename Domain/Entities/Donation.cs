using Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Donation
    {
        public Guid Id { get; set; }
        public Guid DonorId { get; set; }
        public string FoodDetails { get; set; } = default!;
        public string? Quantity { get; set; }
        public DateTime ExpirationDate { get; set; }
        public Status Status { get; set; }
        public PickUp PickUp { get; set; } = null!;
    }
}
