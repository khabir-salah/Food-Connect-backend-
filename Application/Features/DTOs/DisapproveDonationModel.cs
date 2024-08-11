using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.DTOs
{
    public class DisapproveDonationModel
    {
        public Guid Id { get; set; }
        public string ReasonForDisapproval { get; set; }
    }
}
