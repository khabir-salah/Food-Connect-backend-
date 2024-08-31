

using Application.Features.DTOs;
using Domain.Entities;
using static Application.Features.DTOs.ViewDonationCommandModel;

namespace Application.Features.Interfaces.IServices
{
    public interface IDonationService
    {
        Task<PagedResponse<ICollection<DonationResponseCommandModel>>> PageResponse(string route, ViewDonationCommandModel.DonationCommand request);
        
        Task<BaseResponse<ICollection<DonationResponseCommandModel>>> ViewPendingDonationByUser();
        Task<BaseResponse<ICollection<DonationResponseCommandModel>>> ViewReceivedDonationByUser();
        Task<BaseResponse<ICollection<DonationResponseCommandModel>>> ViewClaimedDonationByUser();
        Task<BaseResponse<ICollection<DonationResponseCommandModel>>> ViewDisapprovedDonationByUser();
        Task<BaseResponse<ICollection<DonationResponseCommandModel>>> ViewApprovedDonationByUser();

        Task<BaseResponse<ICollection<DonationResponseCommandModel>>> ExpiredDonationByUser();
        Task<DonationResponseCommandModel> GetDonationByIdAsync(Guid id);
        Task<BaseResponse<ICollection<DonationResponseCommandModel>>> ViewDonationsClaimedByOthers();
    }
}
