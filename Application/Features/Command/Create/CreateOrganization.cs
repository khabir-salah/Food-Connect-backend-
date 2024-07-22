using Application.Features.DTOs;
using Application.Features.Interfaces.IRepositries;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Application.Features.Command.Create.CreateFamily;
using static Application.Features.DTOs.CreateOrganizationCommandModel;

namespace Application.Features.Command.Create
{
    public class CreateOrganization
    {
        public class OraganizationRequestModel : IRequest<BaseResponse<OraganizationResponseModel>>
        {
            public string OganisationName { get; set; } = default!;
            public string PhoneNumber { get; set; } = default!;
            public string Address { get; set; } = default!;
            public string CacNumber { get; set; } = default!;
            public int? Capacity { get; set; }
            public string Email { get; set; } = default!;
            public string Password { get; set; } = default!;
        }

        public class OraganizationResponseModel
        {
            public string OganisationName { get; set; } = default!;
            public string PhoneNumber { get; set; } = default!;
            public string Address { get; set; } = default!;
            public string CacNumber { get; set; } = default!;
            public int? Capacity { get; set; }
            public string Email { get; set; } = default!;
            public Guid RoleId { get; set; }
            public Guid UserId { get; set; }
        }

        public class Handler : IRequestHandler<CreateOrganizationCommand, BaseResponse<CreateOragnizationResponseCommand>>
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

            public async Task<BaseResponse<CreateOragnizationResponseCommand>> Handle(CreateOrganizationCommand request, CancellationToken cancellationToken)
            {
                var checkOrganization = IsEmailExist(request.Email);
                if (!checkOrganization)
                {
                    return new BaseResponse<CreateOragnizationResponseCommand>
                    {
                        Data = null,
                        IsSuccessfull = false,
                        Message = "Organization Registration Failed",
                    };
                }


                var salt = BCrypt.Net.BCrypt.GenerateSalt(10);
                var hashPassword = BCrypt.Net.BCrypt.HashPassword(request.Password, salt);

                var getRole = await _roleRepo.Get(r => r.Name == "Organisation");

                var user = new User
                {
                    Email = request.Email,
                    Password = hashPassword,
                    RoleId = getRole.Id,
                };

                var organization = new Organisation
                {
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

                return new BaseResponse<CreateOragnizationResponseCommand>
                {
                    Message = "Organization Registration Successfull",
                    IsSuccessfull = true,
                    Data = new CreateOragnizationResponseCommand
                    {
                        Email = user.Email,
                        OganisationName = organization.OganisationName,
                        RoleId = getRole.Id,
                        UserId = user.Id
                    }
                };
            }

            private bool IsEmailExist(string email)
            {
                var check = _userRepo.Get(u => u.Email == email);
                return check != null ? true : false;
            }

        }
    }
}
