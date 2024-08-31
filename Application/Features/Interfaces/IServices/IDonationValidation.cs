using Application.Features.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Interfaces.IServices
{
    public interface IDonationValidation
    {
        Task<BaseResponse<string>> ApproveDonationByManager(Guid id);
        Task<BaseResponse<string>> DispproveDonation(DisapproveDonationModel request);
        Task<BaseResponse<string>> ClaimDonation(Guid id);
    }
}
