using Application.Interfaces;
using Application.Queries;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;


namespace Application.Command
{
    public class CreateRecipient
    {
        public class RecipientRequestModel : IRequest<BaseResponse<RecipientResponseModel>>
        {
            public string FirstName { get; set; } = default!;
            public string LastName { get; set; } = default!;
            public string PhoneNumber { get; set; } = default!;
            public string Address { get; set; } = default!;
            public string Nin { get; set; }
            public string Email { get; set; } = default!;
            public string Password { get; set; } = default!;
        }

        public class RecipientResponseModel
        {
            public string FirstName { get; set; } = default!;
            public string LastName { get; set; } = default!;
            public string PhoneNumber { get; set; } = default!;
            public string Address { get; set; } = default!;
            public string Nin { get; set; }
            public string Email { get; set; } = default!;
            public Guid RoleId { get; set; }
            public Guid UserId { get; set; }
        }

        public class Handler : IRequestHandler<RecipientRequestModel, BaseResponse<RecipientResponseModel>>
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

            public async Task<BaseResponse<RecipientResponseModel>> Handle(RecipientRequestModel request, CancellationToken cancellationToken)
            {
                var checkRecipent = IsEmailExist(request.Email);
                if (!checkRecipent)
                {
                    return new BaseResponse<RecipientResponseModel>
                    {
                        Data = null,
                        IsSuccessfull = false,
                        Message = "User Already Exist",
                    };
                }

                var getRole = await _roleRepo.GetAll();
                var role = getRole.Where(r => r.Name == "Organisation").FirstOrDefault();

                var salt = BCrypt.Net.BCrypt.GenerateSalt(10);
                var hashPassword = BCrypt.Net.BCrypt.HashPassword(request.Password, salt);
                var user = new User
                {
                    Email = request.Email,
                    Password = hashPassword,
                    RoleId = role.Id,
                };

                var recipent = new Recipent
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Nin = request.Nin,
                    Address = request.Address,
                    UserId = user.Id,
                    PhoneNumber = request.PhoneNumber,
                };
                _userRepo.Add(user);    
                _recipentRepo.Add(recipent);
                _recipentRepo.Save();
                _logger.LogInformation("User Created Successfully");

                return new BaseResponse<RecipientResponseModel>
                {
                    IsSuccessfull = true,
                    Message = "User Created Successfully",
                    Data = new RecipientResponseModel
                    {
                        Email = user.Email,
                        Address = recipent.Address,
                        FirstName = recipent.FirstName,
                        LastName= recipent.LastName,
                        Nin = recipent.Nin,
                        PhoneNumber= recipent.PhoneNumber,
                        RoleId= user.RoleId,
                        UserId = recipent.UserId
                    }
                };
            }

            private bool IsEmailExist(string email)
            {
                var check = _userRepo.Get(u => u.Email == email);
                return check != null? true: false;
            }
        }
    }
}
