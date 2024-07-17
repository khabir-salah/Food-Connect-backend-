using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Recipent
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
        public string ProfileImage { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
        public string Address { get; set; } = default!;
        public ICollection<FoodCollection?> FoodCollection { get; set; }
        public string Nin { get; set; }
        public string City { get; set; } = default!;        
        public string LOcalGovernment { get; set; } = default!;
        public string PostalCode { get; set; } = default!;
        public string? Capacity { get; set; }
        public ICollection<Donation> Donations { get; set; }
    }
}
