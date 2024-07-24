using Application.Features.DTOs;
using Application.Features.Interfaces.IRepositries;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

using static Application.Features.DTOs.CreateFamilyCommandModel;

namespace Application.Features.Command.Create
{
    public class CreateFamily
    {


        public class Handler : IRequestHandler<CreateFamilyCommand, BaseResponse<CreateFamilyResponseCommand>>
        {
            private readonly IUserRepository _userRepo;
            private readonly IRoleRepository _roleRepo;
            private readonly ILogger<Handler> _logger;
            private readonly IFamilyRepository _familyRepo;

            public Handler(IUserRepository userRepo, IRoleRepository roleRepo, ILogger<Handler> logger, IFamilyRepository familyRepo)
            {
                _userRepo = userRepo;
                _roleRepo = roleRepo;
                _logger = logger;
                _familyRepo = familyRepo;
            }

            public async Task<BaseResponse<CreateFamilyResponseCommand>> Handle(CreateFamilyCommand request, CancellationToken cancellationToken)
            {
                var checkFamily = await _userRepo.IsEmailExist(request.Email);
                if (checkFamily)
                {
                    return new BaseResponse<CreateFamilyResponseCommand>
                    {
                        Data = null,
                        IsSuccessfull = false,
                        Message = "Family Registration Failed",
                    };
                }


                var salt = BCrypt.Net.BCrypt.GenerateSalt(10);
                var hashPassword = BCrypt.Net.BCrypt.HashPassword(request.Password, salt);

                var getRole = await _roleRepo.Get(r => r.Name == "Family");

                var user = new User
                {
                    Email = request.Email,
                    Password = hashPassword,
                    RoleId = getRole.Id,
                };

                var family = new Family
                {
                    FamilyCount = request.FamilyCount,
                    PhoneNumber = request.PhoneNumber,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    UserId = user.Id,
                };

                _familyRepo.Add(family);
                _userRepo.Add(user);
                _familyRepo.Save();
                _logger.LogInformation("Family Registration Successfull");

                return new BaseResponse<CreateFamilyResponseCommand>
                {
                    IsSuccessfull = true,
                    Message = "Family Registration Successfull",
                    Data = new CreateFamilyResponseCommand(user.Id, family.Id, user.RoleId, user.Email, family.FirstName)
                };
            }

        }
    }
}
