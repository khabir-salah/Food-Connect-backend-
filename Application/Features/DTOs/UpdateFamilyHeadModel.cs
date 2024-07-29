using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.DTOs
{
    public class UpdateFamilyHeadModel : IRequest<BaseResponse<string>>
    {
        public string? City { get; set; }
        public string? LOcalGovernment { get; set; }
        public string? PostalCode { get; set; }
        public ICollection<FamilyMemberModel> FamilyMembers { get; set; }
        public string? ProfileImage { get; set; }
        public string? Address { get; set; }
    }
    public class FamilyMemberModel
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string? Nin { get; set; }
        public Guid FamilyHeadId { get; set; }
    }
}
