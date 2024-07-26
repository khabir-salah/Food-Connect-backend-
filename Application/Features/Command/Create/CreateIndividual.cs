using Application.Features.DTOs;
using Application.Features.Interfaces.IRepositries;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using static Application.Features.DTOs.CreateIndividualCommandModel;


namespace Application.Features.Command.Create
{
    public class CreateIndividual
    {

        public class Handler : IRequestHandler<CreateIndividualCommand, BaseResponse<CreateIndividualResponseCommand>>
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

            public async Task<BaseResponse<CreateIndividualResponseCommand>> Handle(CreateIndividualCommand request, CancellationToken cancellationToken)
            {
                var checkRecipent = await _userRepo.IsEmailExist(request.Email);
                if (checkRecipent)
                {
                    return new BaseResponse<CreateIndividualResponseCommand>
                    {
                        Data = null,
                        IsSuccessfull = false,
                        Message = "User Already Exist",
                    };
                }


                var salt = BCrypt.Net.BCrypt.GenerateSalt(10);
                var hashPassword = BCrypt.Net.BCrypt.HashPassword(request.Password, salt);

                var getRole = await _roleRepo.Get(r => r.Name == "Individual");

                var user = new User
                {
                    Email = request.Email,
                    Password = hashPassword,
                    RoleId = getRole.Id,
                    PhoneNumber = request.PhoneNumber,
                };

                var recipent = new Individual
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Nin = request.Nin,
                    UserId = user.Id,
                };
                _userRepo.Add(user);
                _recipentRepo.Add(recipent);
                _recipentRepo.Save();
                _logger.LogInformation("User Created Successfully");

                return new BaseResponse<CreateIndividualResponseCommand>
                {
                    IsSuccessfull = true,
                    Message = "User Created Successfully",
                    Data = new CreateIndividualResponseCommand
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
