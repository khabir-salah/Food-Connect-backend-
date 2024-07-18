using Application.Interfaces;
using Application.Queries;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Application.Command.CreateFamily;

namespace Application.Command
{
    public class CreateOrganization
    {
        public class OraganizationRequestModel : IRequest<BaseResponse<OraganizationResponseModel>>
        {
            public string OganisationName { get; set; } = default!;
            public string PhoneNumber { get; set; } = default!;
            public string Address { get; set; } = default!;
            public string CacNumber { get; set; } = default!;
            public string? Capacity { get; set; }
            public string Email { get; set; } = default!;
            public string Password { get; set; } = default!;
        }

        public class OraganizationResponseModel
        {
            public string OganisationName { get; set; } = default!;
            public string PhoneNumber { get; set; } = default!;
            public string Address { get; set; } = default!;
            public string CacNumber { get; set; } = default!;
            public string? Capacity { get; set; }
            public string Email { get; set; } = default!;
            public Guid RoleId { get; set; }
            public Guid UserId { get; set; }
        }

        public class Handler : IRequestHandler<OraganizationRequestModel, BaseResponse<OraganizationResponseModel>>
        {
            private readonly IUserRepository _userRepo;
            private readonly IRoleRepository _roleRepo;
            private readonly ILogger<Handler> _logger;
            private readonly IOragnisationRepository _organisationRepo;

            public Handler(IUserRepository userRepo, IRoleRepository roleRepo, ILogger<Handler> logger, IOragnisationRepository organisationRepo)
            {
                _userRepo = userRepo;
                _roleRepo = roleRepo;
                _logger = logger;
                _organisationRepo = organisationRepo;
            }

            public async Task<BaseResponse<OraganizationResponseModel>> Handle(OraganizationRequestModel request, CancellationToken cancellationToken)
            {
                var checkOrganization = IsEmailExist(request.Email);
                if (!checkOrganization)
                {
                    return new BaseResponse<OraganizationResponseModel>
                    {
                        Data = null,
                        IsSuccessfull = false,
                        Message = "Organization Registration Failed",
                    };
                }

                var getRole = await _roleRepo.Get(r => r.Name == "Organisation");

                var salt = BCrypt.Net.BCrypt.GenerateSalt(10);
                var hashPassword = BCrypt.Net.BCrypt.HashPassword(request.Password, salt);

                var user = new User
                {
                    Email = request.Email,
                    Password = hashPassword,
                    RoleId = getRole.Id,
                };

                var organization = new Organisation
                {
                    Address = request.Address,
                    CacNumber = request.CacNumber,
                    Capacity = request.Capacity,
                    PhoneNumber = request.PhoneNumber,
                    UserId = user.Id,
                    OganisationName = request.OganisationName,
                };

                _userRepo.Add(user);
                _organisationRepo.Add(organization);
                _organisationRepo.Save();
                _logger.LogInformation("Organization Registration Successfull");
            }

            private bool IsEmailExist(string email)
            {
                var check = _userRepo.Get(u => u.Email == email);
                return check != null ? true : false;
            }
        }
    }
}
