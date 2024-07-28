

using Application.Features.DTOs;
using Domain.Entities;
using static Application.Features.DTOs.ViewDonationCommandModel;

namespace Application.Features.Interfaces.IServices
{
    public interface IDonationService
    {
        Task<PagedResponse<ICollection<DonationResponseCommandModel>>> PageResponse(string route, ViewDonationCommandModel.DonationCommand request);
        Task<BaseResponse<string>> ApproveDonation(Guid id);
        Task<BaseResponse<string>> DispproveDonation(Guid id);
        Task<BaseResponse<ICollection<DonationResponseCommandModel>>> ViewDonationByUser();
    }
}
