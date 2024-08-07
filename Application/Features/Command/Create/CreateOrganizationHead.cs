using Application.Features.DTOs;
using Application.Features.Interfaces.IRepositries;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

using static Application.Features.DTOs.CreateOrganizationCommandModel;

namespace Application.Features.Command.Create
{
    public class CreateOrganizationHead
    {
        //creating a head of an organization
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
                //checking if the user already exist
                var checkOrganization = await _userRepo.IsEmailExist(request.Email);
                if (checkOrganization)
                {
                    return new BaseResponse<CreateOragnizationResponseCommand>
                    {
                        Data = null,
                        IsSuccessfull = false,
                        Message = "Organization Registration Failed",
                    };
                }

                //adding salt and hashing the password
                var salt = BCrypt.Net.BCrypt.GenerateSalt(10);
                var hashPassword = BCrypt.Net.BCrypt.HashPassword(request.Password, salt);

                //assigning role
                var getRole = await _roleRepo.Get(r => r.Name == "OrganisationHead");

                var user = new User
                {
                    Email = request.Email,
                    Password = hashPassword,
                    RoleId = getRole.Id,
                    PhoneNumber = request.PhoneNumber,
                    Name = request.OganisationName  
                };

                var organization = new Organisation
                {
                    CacNumber = request.CacNumber,
                    NumberOfPeopleInOrganization = request.Capacity,
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

           
        }
    }
}
