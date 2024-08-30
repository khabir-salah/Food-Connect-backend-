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
        public string? Nin { get; set; } 
        public string RoleId { get; set; }
        public Guid FamilyHeadId { get; set; }
    }
}
