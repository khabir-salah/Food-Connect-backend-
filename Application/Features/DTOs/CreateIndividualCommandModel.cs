using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.DTOs
{
    public class CreateIndividualCommandModel
    {
        public record CreateIndividualCommand : IRequest<BaseResponse<CreateIndividualResponseCommand>>
        {
            public string FirstName { get; set; } = default!;
            public string LastName { get; set; } = default!;
            public string PhoneNumber { get; set; } = default!;
            public string Nin { get; set; }
            public string Email { get; set; } = default!;
            public string Password { get; set; } = default!;
        }

        public record CreateIndividualResponseCommand
        {
            public string FirstName { get; set; } = default!;
            public string Email { get; set; } = default!;
            public Guid RoleId { get; set; }
            public Guid UserId { get; set; }
        }
    }
}
