using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.DTOs
{
    public class CreateManagerCommandModel
    {
        public record CreateManagerCommand : IRequest<BaseResponse<CreateManagerResponseCommand>>
        {
            public string FirstName { get; set; } = default!;
            public string LastName { get; set; } = default!;
            public string Email { get; set; } = default!;
            public string Password { get; set; } = default!;
        }

        public record CreateManagerResponseCommand
        {
            public string FirstName { get; set; } = default!;
            public Guid Id { get; set; }
            public string Email { get; set; } = default!;
            public string RoleId { get; set; }
            public Guid UserId { get; set; }
        }
    }
}
