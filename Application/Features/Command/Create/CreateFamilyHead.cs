using Application.Features.DTOs;
using Application.Features.Interfaces.IRepositries;
using Domain.Constant;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

using static Application.Features.DTOs.CreateFamilyHeadCommandModel;

namespace Application.Features.Command.Create
{
    public class CreateFamilyHead
    {

        //creating a head of a family. 
        public class Handler : IRequestHandler<CreateFamilyHeadCommand, BaseResponse<CreateFamilyHeadResponseCommand>>
        {
            private readonly IUserRepository _userRepo;
            private readonly IRoleRepository _roleRepo;
            private readonly ILogger<Handler> _logger;
            private readonly IFamilyHeadRepository _familyHeadRepo;

            public Handler(IUserRepository userRepo, IRoleRepository roleRepo, ILogger<Handler> logger, IFamilyHeadRepository familyHeadRepo)
            {
                _userRepo = userRepo;
                _roleRepo = roleRepo;
                _logger = logger;
                _familyHeadRepo = familyHeadRepo;
            }

            public async Task<BaseResponse<CreateFamilyHeadResponseCommand>> Handle(CreateFamilyHeadCommand request, CancellationToken cancellationToken)
            {
                //checking if the user already exit
                var checkFamily = await _userRepo.IsEmailExist(request.Email);
                if (checkFamily)
                {
                    return new BaseResponse<CreateFamilyHeadResponseCommand>
                    {
                        Data = null,
                        IsSuccessfull = false,
                        Message = "Family Registration Failed",
                    };
                }

                //adding a salt and hashing the password
                var salt = BCrypt.Net.BCrypt.GenerateSalt(10);
                var hashPassword = BCrypt.Net.BCrypt.HashPassword(request.Password, salt);

                var getRole = await _roleRepo.Get(r => r.Name == RoleConst.FamilyHead);

                var user = new User
                {
                    Email = request.Email,
                    Password = hashPassword,
                    RoleId = getRole.RoleId,
                    PhoneNumber = request.PhoneNumber,
                    Name = $"{request.LastName} {request.FirstName}",
                };

                var familyHead = new FamilyHead
                {
                    FamilySize = request.FamilyCount,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    UserId = user.Id,
                    NIN = request.NIN,
                };

                _familyHeadRepo.Add(familyHead);
                _userRepo.Add(user);
                _familyHeadRepo.Save();
                _logger.LogInformation("Family Registration Successfull");

                return new BaseResponse<CreateFamilyHeadResponseCommand>
                {
                    IsSuccessfull = true,
                    Message = "Family Registration Successfull",
                    Data = new CreateFamilyHeadResponseCommand(user.Id, familyHead.Id, user.RoleId, user.Email,familyHead.FirstName, familyHead.NIN)
                };
            }

        }
    }
}
