using Application.Interfaces;
using Application.Queries;
using BCrypt.Net;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Command
{
    public class CreateManager
    {
        public class ManagerRequestModel : IRequest<BaseResponse<ManagerResponseModel>>
        {
            public string FirstName { get; set; } = default!;
            public string LastName { get; set; } = default!;
            public string Email { get; set; } = default!;
            public string Password { get; set; } = default!;
        }


        public class ManagerResponseModel
        {
            public Guid Id { get; set; }
            public string FirstName { get; set; } = default!;
            public string LastName { get; set; } = default!;
            public string Email { get; set; } = default!;
            public Guid RoleId { get; set; }
            public Guid UserId { get; set; }
        }

        public class Handler : IRequestHandler<ManagerRequestModel, BaseResponse<ManagerResponseModel>>
        {
            private readonly IManagerRepository _managerRepo;
            private readonly IUserRepository _userRepo;
            private readonly ILogger<Handler> _logger;
            private readonly IRoleRepository _roleRepo;
            public Handler(IManagerRepository managerRepo, IUserRepository userRepo, IRoleRepository roleRepo, ILogger<Handler> logger)
            {
                _managerRepo = managerRepo;
                _userRepo = userRepo;
                _roleRepo = roleRepo;
                _logger = logger;
            }

            public async Task<BaseResponse<ManagerResponseModel>> Handle(ManagerRequestModel request, CancellationToken cancellationToken)
            {
                var isManagerExist = IsManagerExist(request.Email);
                if (!isManagerExist)
                {
                    return new BaseResponse<ManagerResponseModel>
                    {
                        Data = null,
                        IsSuccessfull = false,
                        Message = "Manager already Exist"
                    };
                }
                var getRole = await _roleRepo.Get(r => r.Name == "Manager");
                var salt = BCrypt.Net.BCrypt.GenerateSalt(10);
                var hashPassword = BCrypt.Net.BCrypt.HashPassword(request.Email,salt);
                var user = new User
                {
                    Email = request.Email,
                    Password = hashPassword,
                    RoleId = getRole.Id,
                };
                var manager = new Manager
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    UserId = user.Id,
                };
                _userRepo.Add(user);
                _managerRepo.Add(manager);
                _managerRepo.Save();
                _logger.LogInformation("Successfully Created Manager");
                return new BaseResponse<ManagerResponseModel>
                {
                    Data = new ManagerResponseModel
                    {
                        Id = manager.Id,
                        Email = user.Email,
                        FirstName = manager.FirstName,
                        LastName = manager.LastName,
                        RoleId = user.RoleId,
                        UserId = user.Id,
                    },
                    Message = "Successfully Created Manager",
                    IsSuccessfull = true
                };
            }

            private bool IsManagerExist(string email)
            {
                var checkEmail = _userRepo.Get(m => m.Email == email);
                return checkEmail != null ? true : false;
            }
        }

    }
}
