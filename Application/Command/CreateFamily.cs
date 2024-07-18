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
using static Application.Command.CreateRecipient;

namespace Application.Command
{
    public class CreateFamily
    {
        public class FamilyRequestModel : IRequest<BaseResponse<FamilyResponseModel>>
        {
            public string FirstName { get; set; } = default!;
            public string LastName { get; set; } = default!;
            public int FamilyCount { get; set; }
            public string PhoneNumber { get; set; } = default!;
            public string Address { get; set; } = default!;
            public string Email { get; set; } = default!;
            public string Password { get; set; } = default!;
        }

        public class FamilyResponseModel
        {
            public string FirstName { get; set; } = default!;
            public string LastName { get; set; } = default!;
            public int FamilyCount { get; set; }
            public string PhoneNumber { get; set; } = default!;
            public string Address { get; set; } = default!;
            public string Email { get; set; } = default!;
            public Guid RoleId { get; set; }
            public Guid UserId { get; set; }
        }

        public class Handler : IRequestHandler<FamilyRequestModel, BaseResponse<FamilyResponseModel>>
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

            public async Task<BaseResponse<FamilyResponseModel>> Handle(FamilyRequestModel request, CancellationToken cancellationToken)
            {
                var checkFamily = IsEmailExist(request.Email);
                if (!checkFamily)
                {
                    return new BaseResponse<FamilyResponseModel>
                    {
                        Data = null,
                        IsSuccessfull = false,
                        Message = "Family Registration Failed", 
                    };
                }

                var getRole = await _roleRepo.Get(r => r.Name == "Family");

                var salt = BCrypt.Net.BCrypt.GenerateSalt(10);
                var hashPassword = BCrypt.Net.BCrypt.HashPassword(request.Password, salt);

                var user = new User
                {
                    Email = request.Email,
                    Password = hashPassword,
                    RoleId = getRole.Id,
                };

                var family = new Family
                {
                    Address = request.Address,
                    FamilyCount = request.FamilyCount,
                    PhoneNumber = request.PhoneNumber,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                };

                _familyRepo.Add(family);
                _userRepo.Add(user);
                _familyRepo.Save();
                _logger.LogInformation("Family Registration Successfull");

                return new BaseResponse<FamilyResponseModel>
                {
                    IsSuccessfull = true,
                    Message = "Family Registration Successfull",
                    Data = new FamilyResponseModel
                    {
                        Address = family.Address,
                        Email = user.Email,
                        FamilyCount = family.FamilyCount,
                        FirstName = family.FirstName,
                        LastName = family.LastName,
                        PhoneNumber = family.PhoneNumber,
                        RoleId = user.RoleId,
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
