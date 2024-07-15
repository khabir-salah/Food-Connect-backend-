using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Rating
    {
        public Guid Id { get; set; }
        public Guid DonationId { get; set; }
        public string? Review { get; set; }
        public string[]? ReviewImages { get; set; }
        public Guid RecipentId { get; set; }
    }
}
