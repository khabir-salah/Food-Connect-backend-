using Application.Features.DTOs;
using Application.Features.Interfaces.IRepositries;
using MediatR;
using Microsoft.Extensions.Logging;
using static Application.Features.Command.Create.CreateOrganizationHead;
using static Application.Features.DTOs.LoginCommandModel;

namespace Application.Features.Queries.Get
{
    public class UserLogin
    {

        public class Handler : IRequestHandler<LoginCommand, BaseResponse<LoginResponseCommand>>
        {
            private readonly IUserRepository _userRepo;
            private readonly ILogger<Handler> _logger;

            public Handler(IUserRepository userRepo, ILogger<Handler> logger)
            {
                _userRepo = userRepo;
                _logger = logger;
            }

            public async Task<BaseResponse<LoginResponseCommand>> Handle(LoginCommand request, CancellationToken cancellationToken)
            {
                var getUser = await _userRepo.GetUserAsync( u => u.Email == request.Email);
                if (getUser is null)
                {
                    return new BaseResponse<LoginResponseCommand>
                    {
                        Data = null,
                        IsSuccessfull = false,
                        Message = "Credential not valid",
                    };
                }


                var hashPassword = BCrypt.Net.BCrypt.Verify(request.Password, getUser.Password);
                if (hashPassword)
                {
                    return new BaseResponse<LoginResponseCommand>
                    {
                        IsSuccessfull = true,
                        Message = "Login Successfull",
                        Data = new LoginResponseCommand
                        {
                            Email = getUser.Email,
                            UserId = getUser.Id,
                            Role = getUser.Role.Name,
                        }
                    };
                }
                return new BaseResponse<LoginResponseCommand>
                {
                    Data = null,
                    IsSuccessfull = false,
                    Message = "Credential not valid",
                };
            }

        }
    }
}
