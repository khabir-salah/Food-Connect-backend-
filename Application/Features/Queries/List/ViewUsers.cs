using Application.Features.DTOs;
using Application.Features.Interfaces.IRepositries;
using MediatR;


namespace Application.Features.Queries.List
{
    public class ViewUsers
    {
        
        public class UserDetailsHandler : IRequestHandler<UserDetailsQuery, PagedResponse<ICollection<UserDetailResponseModel>>>
        {
            private readonly IUserRepository _userRepo;
            public UserDetailsHandler(IUserRepository userRepo)
            {
                _userRepo = userRepo;
            }
            public async Task<PagedResponse<ICollection<UserDetailResponseModel>>> Handle(UserDetailsQuery request, CancellationToken cancellationToken)
            {
                PaginationFilter Filter  = new PaginationFilter();

            var users = await _userRepo.GetAllUserDetailsAsync(Filter);
                if (users == null || !users.Any())
                {
                    return new PagedResponse<ICollection<UserDetailResponseModel>>(null, 0, 0, 0)
                    {

                        IsSuccessfull = false,
                        Data = null,
                    };
                }

                var userDetails = users.Select(u => new  UserDetailResponseModel
                {
                    Role = u.Role.Name,
                    Name = u.Name,
                    Email = u.Email,
                    IsActivated = u.IsActivated,
                    PhoneNumber = u.PhoneNumber,
                    UserId = u.Id,
                }).ToList();

                var totalRecords = await _userRepo.CountAsync();

                return new PagedResponse<ICollection<UserDetailResponseModel>>(userDetails, Filter.PageSize, Filter.PageNumber, totalRecords)
                { IsSuccessfull = true
                , Data = userDetails, };

            }
        }
    }
}
