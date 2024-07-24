using Application.Features.DTOs;
using Application.Features.Interfaces.IRepositries;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using static Application.Features.DTOs.CreateRecipientCommandModel;


namespace Application.Features.Command.Create
{
    public class CreateRecipient
    {

        public class Handler : IRequestHandler<CreateRecipientCommand, BaseResponse<CreateRecpientResponseCommand>>
        {
            private readonly IUserRepository _userRepo;
            private readonly IRoleRepository _roleRepo;
            private readonly ILogger<Handler> _logger;
            private readonly IRecipentRepository _recipentRepo;
            public Handler(IUserRepository userRepo, IRoleRepository roleRepo, ILogger<Handler> logger, IRecipentRepository recipentRepo)
            {
                _userRepo = userRepo;
                _roleRepo = roleRepo;
                _logger = logger;
                _recipentRepo = recipentRepo;
            }

            public async Task<BaseResponse<CreateRecpientResponseCommand>> Handle(CreateRecipientCommand request, CancellationToken cancellationToken)
            {
                var checkRecipent = await _userRepo.IsEmailExist(request.Email);
                if (checkRecipent)
                {
                    return new BaseResponse<CreateRecpientResponseCommand>
                    {
                        Data = null,
                        IsSuccessfull = false,
                        Message = "User Already Exist",
                    };
                }


                var salt = BCrypt.Net.BCrypt.GenerateSalt(10);
                var hashPassword = BCrypt.Net.BCrypt.HashPassword(request.Password, salt);

                var getRole = await _roleRepo.Get(r => r.Name == "Recipent");

                var user = new User
                {
                    Email = request.Email,
                    Password = hashPassword,
                    RoleId = getRole.Id,
                };

                var recipent = new Recipent
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Nin = request.Nin,
                    UserId = user.Id,
                    PhoneNumber = request.PhoneNumber,
                };
                _userRepo.Add(user);
                _recipentRepo.Add(recipent);
                _recipentRepo.Save();
                _logger.LogInformation("User Created Successfully");

                return new BaseResponse<CreateRecpientResponseCommand>
                {
                    IsSuccessfull = true,
                    Message = "User Created Successfully",
                    Data = new CreateRecpientResponseCommand
                    {
                        Email = user.Email,
                        FirstName = recipent.FirstName,
                        RoleId = user.RoleId,
                        UserId = recipent.UserId
                    }
                };
            }

            
        }
    }
}
