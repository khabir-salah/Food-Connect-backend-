using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.DTOs
{
    public class IndividualUpdateCommandModel : IRequest<BaseResponse<string>>
    {
        public string? City { get; set; }
        public string? LOcalGovernment { get; set; }
        public string? PostalCode { get; set; }
        public string? ProfileImage { get; set; }
        public string PhoneNumber { get; set; } 
        public string? Address { get; set; }

    }

    
}
