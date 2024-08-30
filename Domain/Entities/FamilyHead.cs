using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class FamilyHead : Auditables
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
        public int FamilySize { get; set; }
        public string? City { get; set; }
        public string NIN { get; set; }
        public string? LOcalGovernment { get; set; }
        public string? PostalCode { get; set; }
        public ICollection<Family?> Families { get; set; }
        public ICollection<Donation?> Donation { get; set; }
        public ICollection<FoodCollection?> FoodCollection { get; set; }
    }
}
