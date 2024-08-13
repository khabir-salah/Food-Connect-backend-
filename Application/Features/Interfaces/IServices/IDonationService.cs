

using Application.Features.DTOs;
using Domain.Entities;
using static Application.Features.DTOs.ViewDonationCommandModel;

namespace Application.Features.Interfaces.IServices
{
    public interface IDonationService
    {
        Task<PagedResponse<ICollection<DonationResponseCommandModel>>> PageResponse(string route, ViewDonationCommandModel.DonationCommand request);
        Task<BaseResponse<string>> ApproveDonationByAdmin(Guid id);
        Task<BaseResponse<string>> ApproveDonationByManager(Guid id);
        Task<BaseResponse<string>> DispproveDonationByAdmin(DisapproveDonationModel request);
        Task<BaseResponse<string>> DispproveDonation(DisapproveDonationModel request);
        Task<BaseResponse<ICollection<DonationResponseCommandModel>>> ViewPendingDonationByUser();
        Task<BaseResponse<ICollection<DonationResponseCommandModel>>> ViewReceivedDonationByUser();
        Task<BaseResponse<ICollection<DonationResponseCommandModel>>> ViewClaimedDonationByUser();
        Task<BaseResponse<ICollection<DonationResponseCommandModel>>> ViewDisapprovedDonationByUser();
        Task<BaseResponse<ICollection<DonationResponseCommandModel>>> ViewApprovedDonationByUser();
        Task<BaseResponse<string>> ClaimDonation(Guid id);
        Task<int> DisapprovedDonationCountAsync();
        Task<int> ApprovedDonationCountAsync();
        Task<int> PendingDonationCountAsync();
        Task<int> ReceivedDonationCountAsync();
        Task<DonationResponseCommandModel> GetDonationByIdAsync(Guid id);
        Task<PagedResponse<ICollection<DonationResponseCommandModel>>> GetAllDonationsForUser(PaginationFilter filter);
        Task<PagedResponse<ICollection<DonationResponseCommandModel>>> DonationPageResponse(string route, PaginationFilter filter);
        Task<PagedResponse<ICollection<DonationResponseCommandModel>>> SearchDonations(DonationSearchCommand request);
        Task<BaseResponse<string>> DonationReceived(Guid id);
    }
}
