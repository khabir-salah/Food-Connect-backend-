using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.DTOs
{
    public class DonationSearchCommand
    {
        public string? Location { get; set; }
        public int? MinQuantity { get; set; }
        public int? MaxQuantity { get; set; }
        public PaginationFilter Filter { get; set; } = new PaginationFilter();
    }
}
