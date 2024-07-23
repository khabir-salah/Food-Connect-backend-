using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.DTOs
{
    public class CreateOrganizationCommandModel
    {
        public record CreateOrganizationCommand : IRequest<BaseResponse<CreateOragnizationResponseCommand>>
        {
            public string OganisationName { get; set; } = default!;
            public string PhoneNumber { get; set; } = default!;
            public string CacNumber { get; set; } = default!;
            public int? Capacity { get; set; }
            public string Email { get; set; } = default!;
            public string Password { get; set; } = default!;
        }

        public record CreateOragnizationResponseCommand
        {
            public string OganisationName { get; set; } = default!;
            public string Email { get; set; } = default!;
            public Guid RoleId { get; set; }
            public Guid UserId { get; set; }
        }
    }
}
