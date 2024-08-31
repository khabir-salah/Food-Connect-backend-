using Application.Features.DTOs;
using Application.Features.Interfaces.IRepositries;
using Application.Features.Interfaces.IServices;
using Application.Features.Queries.GeneralServices;
using Application.SignalR;
using Domain.Constant;
using Domain.Entities;

using static Application.Features.DTOs.ViewDonationCommandModel;

namespace Application.Features.Queries.Get
{
    public class DonationFilter : IDonationFilter
    {
        private readonly IDonationRepository _donationRepo;
        private readonly IUriService _uriService;
        private readonly ICurrentUser _user;

        public DonationFilter(IDonationRepository donationRepo, IUriService uriService, ICurrentUser user)
        {
            _donationRepo = donationRepo;
            _uriService = uriService;
            _user = user;
        }

        public async Task<PagedResponse<ICollection<DonationResponseCommandModel>>> GetAllDonationsForUser()
        {
            PaginationFilter filter = new PaginationFilter();
            var loggedinUser = await _user.LoggedInUser();
            var allDonations = await _donationRepo.GetAllDonationByPage(filter);
            var available = allDonations.Where(s => s.Status == Domain.Enum.DonationStatus.Available || s.Status == Domain.Enum.DonationStatus.Approve);
            await ExpireDonations();

            var donationsWithClaimStatus = available.Select(d =>
            {
                var canClaim = CanUserClaimDonation(loggedinUser, d);
                return new DonationResponseCommandModel
                {
                    FoodDetails = d.FoodDetails,
                    Status = d.Status,
                    ExpirationDate = d.ExpirationDate,
                    PickUpLocation = d.PickUpLocation,
                    PickUpTime = d.PickUpTime,
                    Quantity = d.Quantity,
                    UserEmail = d.User.Email,
                    UserRole = d.User.Role.Name,
                    Name = d.User.Name,
                    DonationImages = d.Images,
                    PrimaryImageUrl = d.PrimaryImageUrl,
                    DonationMadeBy = d.DonationMadeBy.ToString(),
                    CanClaim = canClaim,
                    DonationId = d.Id,
                    ClaimRestrictionReason = canClaim ? string.Empty : GetClaimRestrictionReason(loggedinUser, d)
                };
            }).ToList();

            var totalRecords = await _donationRepo.CountAsync(Domain.Enum.DonationStatus.Approve);

            return new PagedResponse<ICollection<DonationResponseCommandModel>>(donationsWithClaimStatus, filter.PageNumber, filter.PageSize, totalRecords)
            {
                IsSuccessfull = true,
                Message = "All Available Donations",
                Data = donationsWithClaimStatus
            };
        }

        private bool CanUserClaimDonation(User loggedinUser, Donation donation)
        {
            if (loggedinUser.Id == donation.UserId)
            {
                return false;
            }

            if (loggedinUser.Role.Name == RoleConst.OrganizationHead && donation.Quantity >= 21)
            {
                return true;
            }
            if (loggedinUser.Role.Name == RoleConst.FamilyHead && (donation.Quantity >= 3 && donation.Quantity <= 20))
            {
                return true;
            }
            if (loggedinUser.Role.Name == RoleConst.Individual && (donation.Quantity >= 1 && donation.Quantity <= 3))
            {
                return true;
            }
            return false;
        }


        private string GetClaimRestrictionReason(User loggedinUser, Donation donation)
        {
            if (loggedinUser.Id == donation.UserId)
            {
                return "You Can't Claim Donation Made By You";
            }

            if (loggedinUser.Role.Name == RoleConst.OrganizationHead)
            {
                if (donation.Quantity < 21)
                {
                    return "Organizations can only claim donations with a quantity of 21 or more.";
                }
            }
            if (loggedinUser.Role.Name == RoleConst.FamilyHead)
            {
                if (donation.Quantity < 3 || donation.Quantity > 20)
                {
                    return "Families can only claim donations with a quantity between 3 and 20.";
                }
            }
            if (loggedinUser.Role.Name == RoleConst.Individual)
            {
                if (donation.Quantity < 1 || donation.Quantity > 3)
                {
                    return "Individuals can only claim donations with a quantity between 1 and 3.";
                }
            }
            return "You are not eligible to claim this donation.";
        }

        public async Task<PagedResponse<ICollection<DonationResponseCommandModel>>> SearchDonations(DonationSearchCommand request)
        {
            var loggedinUser = await _user.LoggedInUser();
            var donations = await _donationRepo.GetAllDonationByPage(request.Filter);

            if(!donations.Any())
            {
                return new PagedResponse<ICollection<DonationResponseCommandModel>>(new List<DonationResponseCommandModel>(), request.Filter.PageNumber, request.Filter.PageSize, 0)
                {
                    IsSuccessfull = false,
                    Message = "Filtered Donations",
                    Data = new List<DonationResponseCommandModel>()
                };
            }

            if (!string.IsNullOrEmpty(request.Location))
            {
                donations = donations.Where(d => d.PickUpLocation.Contains(request.Location, StringComparison.OrdinalIgnoreCase)).ToList();
            }


            if (request.MinQuantity.HasValue)
            {
                donations = donations.Where(d => d.Quantity >= request.MinQuantity.Value).ToList();
            }

            if (request.MaxQuantity.HasValue)
            {
                donations = donations.Where(d => d.Quantity <= request.MaxQuantity.Value).ToList();
            }

            var donationsWithClaimStatus = donations.Select(d =>
            {
                var canClaim = CanUserClaimDonation(loggedinUser, d);
                return new DonationResponseCommandModel
                {
                    FoodDetails = d.FoodDetails,
                    Status = d.Status,
                    ExpirationDate = d.ExpirationDate,
                    PickUpLocation = d.PickUpLocation,
                    PickUpTime = d.PickUpTime,
                    Quantity = d.Quantity,
                    UserEmail = d.User.Email,
                    UserRole = d.User.Role.Name,
                    Address = d.User.Address,
                    Name = d.User.Name,
                    PhoneNumber = d.User.PhoneNumber,
                    PrimaryImageUrl = d.PrimaryImageUrl,
                    DonationImages = d.Images,
                    DonationMadeBy = d.DonationMadeBy.ToString(),
                    CanClaim = canClaim,
                    ClaimRestrictionReason = canClaim ? string.Empty : GetClaimRestrictionReason(loggedinUser, d)
                };
            }).ToList();

            var totalRecords = donations.Count();

            return new PagedResponse<ICollection<DonationResponseCommandModel>>(donationsWithClaimStatus, request.Filter.PageNumber, request.Filter.PageSize, totalRecords)
            {
                IsSuccessfull = true,
                Message = "Filtered Donations",
                Data = donationsWithClaimStatus
            };
        }

        public async Task<PagedResponse<ICollection<DonationResponseCommandModel>>> DonationPageResponse(string route, PaginationFilter filter)
        {
            var filterPage = new PaginationFilter(filter.PageNumber, filter.PageSize);
            var pagedData = await GetAllDonationsForUser();
            var pagedResponse = PaginationHelper.CreatePagedReponse(pagedData.Data, filterPage, _uriService, route);
            return pagedResponse;
        }

        private async Task ExpireDonations()
        {
            var donations = await _donationRepo.GetAll();

            var expiredDonations = donations.Where(d => d.ExpirationDate <= DateTime.UtcNow && d.Status == Domain.Enum.DonationStatus.Approve && d.Status == Domain.Enum.DonationStatus.Available);

            foreach (var donation in expiredDonations)
            {
                donation.Status = Domain.Enum.DonationStatus.Expired;
            }
            _donationRepo.Save();
        }

        private async Task<PagedResponse<ICollection<DonationResponseCommandModel>>> ClaimableDonations()
        {
            PaginationFilter filter = new PaginationFilter();   
            var donation = await GetAllDonationsForUser();
            var claim = donation.Data.Where(C => C.CanClaim == true).ToList();
            if(!claim.Any())
            {
                return new PagedResponse<ICollection<DonationResponseCommandModel>>(null, filter.PageSize, filter.PageNumber, filter.TotalRecords)
                {
                    IsSuccessfull = false,

                };
            }
            return new PagedResponse<ICollection<DonationResponseCommandModel>>(claim, filter.PageNumber, filter.PageSize, filter.TotalRecords)
            {
                IsSuccessfull = true,
                Message = "All Claimable Donations",
                Data = claim
            };

        }

        public async Task<PagedResponse<ICollection<DonationResponseCommandModel>>> ClaimableDonationPageResponse(string route, PaginationFilter filter)
        {
            var filterPage = new PaginationFilter(filter.PageNumber, filter.PageSize);
            var pagedData = await ClaimableDonations();
            var pagedResponse = PaginationHelper.CreatePagedReponse(pagedData.Data, filterPage, _uriService, route);
            return pagedResponse;
        }

        public async Task<BaseResponse<ICollection<DonationResponseCommandModel>>> AllDonationSearch(DonationSearchCommand request)
        {
            var donations = await _donationRepo.GetAll();

            donations = donations.Where(d => d.PickUpLocation.Contains(request.Location, StringComparison.OrdinalIgnoreCase) && d.Quantity >= request.MinQuantity.Value && d.Quantity <= request.MaxQuantity.Value).ToList();
            
            var donationsWithStatus = donations.Select(d =>
            {
                return new DonationResponseCommandModel
                {
                    FoodDetails = d.FoodDetails,
                    Status = d.Status,
                    ExpirationDate = d.ExpirationDate,
                    PickUpLocation = d.PickUpLocation,
                    PickUpTime = d.PickUpTime,
                    Quantity = d.Quantity,
                    UserEmail = d.User.Email,
                    UserRole = d.User.Role.Name,
                    Address = d.User.Address,
                    Name = d.User.Name,
                    PhoneNumber = d.User.PhoneNumber,
                    PrimaryImageUrl = d.PrimaryImageUrl,
                    DonationImages = d.Images,
                    DonationMadeBy = d.DonationMadeBy.ToString(),
                };
            }).ToList();

            return new BaseResponse<ICollection<DonationResponseCommandModel>>
            {
                IsSuccessfull = true,
                Message = "Filtered Donations",
                Data = donationsWithStatus
            };
        }
    }
}
