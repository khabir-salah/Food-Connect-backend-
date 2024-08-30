using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Individual : Auditables
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
        public ICollection<FoodCollection?> FoodCollection { get; set; }
        public string Nin { get; set; } = null!;
        public string? City { get; set; }       
        public string? LOcalGovernment { get; set; } 
        public string? PostalCode { get; set; } 
        public int NumberOfPersons { get; set; } = 1;
        public ICollection<Donation> Donations { get; set; }
    }
}
