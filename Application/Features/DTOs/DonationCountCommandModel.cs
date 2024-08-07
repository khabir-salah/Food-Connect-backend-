using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.DTOs
{
    public class DonationCountCommandModel
    {
        public int PendingCount { get; set; }
        public int ApproveCount { get; set; }
        public int DisapproveCount { get; set; }
        public int ReceivedCount { get; set; }

    }
}
