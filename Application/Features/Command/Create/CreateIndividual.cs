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
        // creating an individual user
        public class Handler : IRequestHandler<CreateIndividualCommand, BaseResponse<CreateIndividualResponseCommand>>
        {
            private readonly IUserRepository _userRepo;
            private readonly IRoleRepository _roleRepo;
            private readonly ILogger<Handler> _logger;
            private readonly IIndividualRepository _recipentRepo;
            public Handler(IUserRepository userRepo, IRoleRepository roleRepo, ILogger<Handler> logger, IIndividualRepository recipentRepo)
            {
                _userRepo = userRepo;
                _roleRepo = roleRepo;
                _logger = logger;
                _recipentRepo = recipentRepo;
            }

            public async Task<BaseResponse<CreateIndividualResponseCommand>> Handle(CreateIndividualCommand request, CancellationToken cancellationToken)
            {
                //checking if a user already exist
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

                //adding salt to the password and hashing it
                var salt = BCrypt.Net.BCrypt.GenerateSalt(10);
                var hashPassword = BCrypt.Net.BCrypt.HashPassword(request.Password, salt);

                var getRole = await _roleRepo.Get(r => r.Name == "Individual");

                var user = new User
                {
                    Email = request.Email,
                    Password = hashPassword,
                    RoleId = getRole.RoleId,
                    PhoneNumber = request.PhoneNumber,
                    Name = $"{request.FirstName} {request.LastName}",
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
