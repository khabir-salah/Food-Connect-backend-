using Application.Features.DTOs;
using Application.Features.Interfaces.IRepositries;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using static Application.Features.DTOs.CreateManagerCommandModel;

namespace Application.Features.Command.Create
{
    public class CreateManager
    {
        //creating  a manager
        public class Handler : IRequestHandler<CreateManagerCommand, BaseResponse<CreateManagerResponseCommand>>
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

            public async Task<BaseResponse<CreateManagerResponseCommand>> Handle(CreateManagerCommand request, CancellationToken cancellationToken)
            {
                //checking if the manager already exist
                var isManagerExist = await _userRepo.IsEmailExist(request.Email);
                if (isManagerExist)
                {
                    return new BaseResponse<CreateManagerResponseCommand>
                    {
                        Data = null,
                        IsSuccessfull = false,
                        Message = "Manager already Exist"
                    };
                }

                // adding salt and hashing the password
                var salt = BCrypt.Net.BCrypt.GenerateSalt(10);
                var hashPassword = BCrypt.Net.BCrypt.HashPassword(request.Email, salt);

                //assigning role to user
                var getRole = await _roleRepo.Get(r => r.Name == "Manager");

                var user = new User
                {
                    Email = request.Email,
                    Password = hashPassword,
                    RoleId = getRole.Id,
                    Name = $"{request.FirstName} {request.LastName}",
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
                return new BaseResponse<CreateManagerResponseCommand>
                {
                    Data = new CreateManagerResponseCommand
                    {
                        Id = manager.Id,
                        Email = user.Email,
                        RoleId = user.RoleId,
                        UserId = user.Id,
                        FirstName = request.FirstName,
                    },
                    Message = "Successfully Created Manager",
                    IsSuccessfull = true
                };
            }

        }

    }
}
