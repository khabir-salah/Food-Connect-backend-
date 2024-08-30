using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.DTOs
{
    public class MarkReceiveModel
    {
        public Guid DonationId { get; set; }
        public string Review { get; set; }
    }

    public class DonationAvailbaleModel
    {
        public Guid DonationId { get; set; }
        public string reason { get; set; }
    }
}
