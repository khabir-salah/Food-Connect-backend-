

using Application.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using static Application.Command.CreateOrganization;

namespace Application.Queries
{
    public class UserLogin
    {
        public class LoginRequestModel : IRequest<BaseResponse<LoginResponseModel>>
        {
            public string Email { get; set; } 
            public string Password { get; set; }   
        }

        public class LoginResponseModel
        {
            public string Email { get; set; }
            public string Password { get; set; }
            public Guid RoleId { get; set; }
            public Guid UserId { get; set; }
        }

        public class Handler : IRequestHandler<LoginRequestModel, BaseResponse<LoginResponseModel>>
        {
            private readonly IUserRepository _userRepo;
            private readonly ILogger<Handler> _logger;

            public Handler(IUserRepository userRepo, ILogger<Handler> logger)
            {
                _userRepo = userRepo;
                _logger = logger;
            }
        
            public async Task<BaseResponse<LoginResponseModel>> Handle(LoginRequestModel request, CancellationToken cancellationToken)
            {
                var getUser = await _userRepo.Get(u => u.Email == request.Email);
                if (getUser is null)
                {
                    return  new BaseResponse<LoginResponseModel>
                    {
                        Data = null,
                        IsSuccessfull = false,
                        Message = "Credential not valid",
                    };
                }


                var hashPassword = BCrypt.Net.BCrypt.Verify(request.Password, getUser.Password);
                if(hashPassword)
                {
                    return new BaseResponse<LoginResponseModel>
                    {
                        IsSuccessfull = true,
                        Message = "Login Successfull",
                        Data = new LoginResponseModel
                        {
                            Email = getUser.Email,
                            Password = getUser.Password,
                            RoleId = getUser.RoleId,
                            UserId = getUser.Id
                        }
                    };
                }
                return new BaseResponse<LoginResponseModel>
                {
                    Data = null,
                    IsSuccessfull = false,
                    Message = "Credential not valid",
                };
            }

        }
    }
}
