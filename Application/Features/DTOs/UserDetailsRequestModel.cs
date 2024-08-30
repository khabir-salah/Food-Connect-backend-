using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.DTOs
{
    public record UserDetailsQuery : IRequest<PagedResponse<ICollection<UserDetailResponseModel>>>
    {
    }

    public record UserDetailResponseModel
    {
        public string Email { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
        public string? Name { get; set; }
        public bool IsActivated { get; set; }
        public string Role { get; set; }
        public Guid UserId { get; set; }
    }
}
