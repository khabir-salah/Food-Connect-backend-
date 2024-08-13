using Application.Features.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Interfaces.IServices
{
    public interface IViewProfile
    {
        Task<BaseResponse<ViewProfileCommandModel>> ViewProfileAsync(Guid Id);
    }
}
