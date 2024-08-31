using Application.Features.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Interfaces.IServices
{
    public interface IUserService
    {
        Task<PagedResponse<ICollection<UserDetailResponseModel>>> PageResponse(string route, PaginationFilter filter);
        Task<BaseResponse<UserDetailModel>> GetUserByIdAsync(Guid UserId);
    }
}
