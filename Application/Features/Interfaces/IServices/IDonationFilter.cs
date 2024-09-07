using Application.Features.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Application.Features.DTOs.ViewDonationCommandModel;

namespace Application.Features.Interfaces.IServices
{
    public interface IDonationFilter
    {
        Task<BaseResponse<ICollection<DonationResponseCommandModel>>> SearchDonations(DonationSearchCommand request);
        Task<BaseResponse<ICollection<DonationResponseCommandModel>>> GetAllDonationsForUser( );
        Task<PagedResponse<ICollection<DonationResponseCommandModel>>> ClaimableDonationPageResponse(string route, PaginationFilter filter);
        Task<PagedResponse<ICollection<DonationResponseCommandModel>>> DonationPageResponse(string route, PaginationFilter filter);
        Task<BaseResponse<ICollection<DonationResponseCommandModel>>> AllDonationSearch(DonationSearchCommand request);
    }
}
