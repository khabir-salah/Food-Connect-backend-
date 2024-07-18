using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Family : Auditables
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
        public string ProfileImage { get; set; } = default!;

        public ICollection<string?> Nin { get; set; } 
        public int FamilyCount { get; set; }
        public string PhoneNumber { get; set; } = default!;
        public string Address { get; set; } = default!;
        public string City { get; set; } = default!;
        public string LOcalGovernment { get; set; } = default!;
        public string PostalCode { get; set; } = default!;
        public ICollection<Donation?> Donation { get; set; }
        public ICollection<FoodCollection?> FoodCollection { get; set; }
    }
}
