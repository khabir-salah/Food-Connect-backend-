using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Organisation : Auditables
    {
        public string OganisationName { get; set; } = default!;
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
        public string CacNumber { get; set; } = default!;
        public string? City { get; set; } 
        public string? LOcalGovernment { get; set; } 
        public string? PostalCode { get; set; } 
        public int? NumberOfPeopleInOrganization { get; set; }
        public ICollection<Donation> Donations { get; set; }
        public ICollection<FoodCollection?> FoodCollection { get; set; }

    }
}
