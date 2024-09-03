 using Application.Features.DTOs;
using Application.Features.Interfaces.IRepositries;
using Application.Features.Interfaces.IServices;
using Application.Features.Queries.GeneralServices;
using Application.SignalR;
using Domain.Constant;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using static Application.Features.DTOs.ViewDonationCommandModel;

namespace Application.Features.Queries.Get
{ 
    public class DonationService : IDonationService
    {
        private readonly IDonationRepository _donationRepo;
        private readonly IUriService _uriService;
        private readonly IMediator _mediator;
        private readonly ICurrentUser _user;
        private readonly IUserRepository _userRepository;
        private readonly IHubContext<NotificationHub> _hubContext;

        public DonationService(IDonationRepository donationRepo, IUriService uriService, IMediator mediator, ICurrentUser user, IUserRepository userRepository,IHubContext<NotificationHub> hubContext)
        {
            _donationRepo = donationRepo;
            _uriService = uriService;
            _mediator = mediator;
            _user = user;
            _userRepository = userRepository;
            _hubContext = hubContext;
        }


        public async Task<PagedResponse<ICollection<DonationResponseCommandModel>>> PageResponse(string route, ViewDonationCommandModel.DonationCommand request)
        {
            var filter = new PaginationFilter(request.filter.PageNumber, request.filter.PageSize);
            var pagedData = await _mediator.Send(request);
            var pagedResponse = PaginationHelper.CreatePagedReponse(pagedData.Data, filter, _uriService, route);
            return pagedResponse;
        }


        public async Task<BaseResponse<ICollection<DonationResponseCommandModel>>> ViewPendingDonationByUser()
        {
            var user = await _user.LoggedInUser();
            var donation = await _donationRepo.GetDonationByUserAsync(d => d.UserId == user.Id);
            var donationType = donation.Where(t => t.Status == Domain.Enum.DonationStatus.pending);
            if (donationType == null)
            {
                return new BaseResponse<ICollection<DonationResponseCommandModel>>
                {
                    Data = null,
                    IsSuccessfull = false,
                    Message = "No donation Created yet. create a Donation now"
                };
            }
            return new BaseResponse<ICollection<DonationResponseCommandModel>>
            {
                IsSuccessfull = true,
                Message = "List of Donations",
                Data = donationType.Select(s => new DonationResponseCommandModel
                {
                    DonationImages = s.Images,
                    ExpirationDate = s.ExpirationDate,
                    FoodDetails = s.FoodDetails,
                    PickUpLocation = s.PickUpLocation,
                    PickUpTime = s.PickUpTime,
                    Quantity = s.Quantity,
                    Status = s.Status,
                    PrimaryImageUrl = s.PrimaryImageUrl,
                }).ToList()
            };
        }

        public async Task<BaseResponse<ICollection<DonationResponseCommandModel>>> ViewApprovedDonationByUser()
        {
            var user = await _user.LoggedInUser();
            var donation = await _donationRepo.GetDonationByUserAsync(d => d.UserId == user.Id);
            var donationType = donation.Where(t => t.Status == Domain.Enum.DonationStatus.Approve);
            if (donationType == null)
            {
                return new BaseResponse<ICollection<DonationResponseCommandModel>>
                {
                    Data = null,
                    IsSuccessfull = false,
                    Message = "No donation Created yet. create a survey now"
                };
            }
            return new BaseResponse<ICollection<DonationResponseCommandModel>>
            {
                IsSuccessfull = true,
                Message = "List of Donations",
                Data = donationType.Select(s => new DonationResponseCommandModel
                {
                    DonationImages = s.Images,
                    ExpirationDate = s.ExpirationDate,
                    FoodDetails = s.FoodDetails,
                    PickUpLocation = s.PickUpLocation,
                    PickUpTime = s.PickUpTime,
                    Quantity = s.Quantity,
                    Status = s.Status,
                    PrimaryImageUrl = s.PrimaryImageUrl,
                }).ToList()
            };
        }

        public async Task<BaseResponse<ICollection<DonationResponseCommandModel>>> ViewClaimedDonationByUser()
         {

            var user = await _user.LoggedInUser();
            //var donationUser = await _userRepository.GetUserDonationAsync(u => u.Id == user.Id);
            var donationUser = await _donationRepo.GetDonationByUserAsync(d => d.UserId == user.Id);


            if (donationUser == null)
            {
                return new BaseResponse<ICollection<DonationResponseCommandModel>>
                {
                    Data = null,
                    IsSuccessfull = false,
                    Message = "No food donation created or claimed yet."
                };
            }

                                                
            var claimedDonations = donationUser.Where(t => t.Status == Domain.Enum.DonationStatus.Claim).ToList();


            if (!claimedDonations.Any())
            {
                return new BaseResponse<ICollection<DonationResponseCommandModel>>
                {
                    Data = null,
                    IsSuccessfull = false,
                    Message = "No claimed donations found."
                };
            }

            var data = new List<DonationResponseCommandModel>();

            foreach (var donation in claimedDonations)
            {
                var recipient = await _userRepository.GetUserAsync(u => u.Id == donation.Recipient);

                var donationResponse = new DonationResponseCommandModel
                {
                    DonationImages = donation.Images,
                    ExpirationDate = donation.ExpirationDate,
                    FoodDetails = donation.FoodDetails,
                    PickUpLocation = donation.PickUpLocation,
                    PickUpTime = donation.PickUpTime,
                    Quantity = donation.Quantity,
                    Status = donation.Status,
                    PrimaryImageUrl = donation.PrimaryImageUrl,
                    RecipientName = recipient?.Name,
                    RecipientEmail = recipient?.Email,
                    Address = recipient?.Address,
                    RecipientRole = recipient?.Role.Name,
                    DonationId = donation.Id,
                    UserEmail = user.Email
                };

                data.Add(donationResponse);
            }

            return new BaseResponse<ICollection<DonationResponseCommandModel>>
            {
                Data = data,
                IsSuccessfull = true,
                Message = "Claimed donations retrieved successfully."
            };
        }


        public async Task<BaseResponse<ICollection<DonationResponseCommandModel>>> ViewDonationsClaimedByOthers()
        {
            var user = await _user.LoggedInUser();
            var donationUser = await _donationRepo.GetDonationByUserAsync(u => u.Recipient == user.Id);

            if (donationUser == null || !donationUser.Any())
            {
                return new BaseResponse<ICollection<DonationResponseCommandModel>>
                {
                    Data = null,
                    IsSuccessfull = false,
                    Message = "No food donation created or claimed yet."
                };
            }

            var donationsClaimedByOthers = donationUser
                                                        .Where(d => d.Status == Domain.Enum.DonationStatus.Claim && d.Recipient != null)
                                                        .ToList();

            if (!donationsClaimedByOthers.Any())
            {
                return new BaseResponse<ICollection<DonationResponseCommandModel>>
                {
                    Data = null,
                    IsSuccessfull = false,
                    Message = "No donations claimed by others found."
                };
            }

            var data = new List<DonationResponseCommandModel>();

            foreach (var donation in donationsClaimedByOthers)
            {
                var recipient = await _userRepository.GetUserAsync(u => u.Id == donation.Recipient);

                var donationResponse = new DonationResponseCommandModel
                {
                    DonationImages = donation.Images,
                    ExpirationDate = donation.ExpirationDate,
                    FoodDetails = donation.FoodDetails,
                    PickUpLocation = donation.PickUpLocation,
                    PickUpTime = donation.PickUpTime,
                    Quantity = donation.Quantity,
                    Status = donation.Status,
                    PrimaryImageUrl = donation.PrimaryImageUrl,
                    RecipientName = recipient?.Name,
                    RecipientEmail = recipient?.Email,
                    Address = recipient?.Address,
                    RecipientRole = recipient?.Role.Name,
                    DonationId = donation.Id
                };

                data.Add(donationResponse);
            }

            return new BaseResponse<ICollection<DonationResponseCommandModel>>
            {
                Data = data,
                IsSuccessfull = true,
                Message = "Donations claimed by others retrieved successfully."
            };
        }





        public async Task<BaseResponse<ICollection<DonationResponseCommandModel>>> ViewReceivedDonationByUser()
        {
            var user = await _user.LoggedInUser();
            var donation = await _donationRepo.GetDonationByUserAsync(d => d.UserId == user.Id);
            var donationType = donation.Where(t => t.Status == Domain.Enum.DonationStatus.Received);
            if (donationType == null)
            {
                return new BaseResponse<ICollection<DonationResponseCommandModel>>
                {
                    Data = null,
                    IsSuccessfull = false,
                    Message = "No donation Created yet. create a survey now"
                };
            }
            return new BaseResponse<ICollection<DonationResponseCommandModel>>
            {
                IsSuccessfull = true,
                Message = "List of Donations",
                Data = donationType.Select(s => new DonationResponseCommandModel
                {
                    DonationImages = s.Images,
                    ExpirationDate = s.ExpirationDate,
                    FoodDetails = s.FoodDetails,
                    PickUpLocation = s.PickUpLocation,
                    PickUpTime = s.PickUpTime,
                    Quantity = s.Quantity,
                    Status = s.Status,
                    PrimaryImageUrl = s.PrimaryImageUrl,
                }).ToList()
            };
        }

        public async Task<BaseResponse<ICollection<DonationResponseCommandModel>>> ViewDisapprovedDonationByUser()
        {
            var user = await _user.LoggedInUser();
            var donation = await _donationRepo.GetDonationByUserAsync(d => d.UserId == user.Id);
            var donationType = donation.Where(t => t.Status == Domain.Enum.DonationStatus.Received);
            if (donationType == null)
            {
                return new BaseResponse<ICollection<DonationResponseCommandModel>>
                {
                    Data = null,
                    IsSuccessfull = false,
                    Message = "No donation Created yet. create a survey now"
                };
            }
            return new BaseResponse<ICollection<DonationResponseCommandModel>>
            {
                IsSuccessfull = true,
                Message = "List of Donations",
                Data = donationType.Select(s => new DonationResponseCommandModel
                {
                    DonationImages = s.Images,
                    ExpirationDate = s.ExpirationDate,
                    FoodDetails = s.FoodDetails,
                    PickUpLocation = s.PickUpLocation,
                    PickUpTime = s.PickUpTime,
                    Quantity = s.Quantity,
                    Status = s.Status,
                    PrimaryImageUrl = s.PrimaryImageUrl,
                    ReasonForDisapproval = s.ReasonForDisapproval,
                }).ToList()
            };
        }

        
        public async Task<DonationResponseCommandModel> GetDonationByIdAsync(Guid id)
        {
            var donation = await _donationRepo.Get(d => d.Id == id);
            return new DonationResponseCommandModel
            {
                PickUpLocation = donation.PickUpLocation,
                DonationImages = donation.Images,
                FoodDetails = donation.FoodDetails,
                ExpirationDate = donation.ExpirationDate,
                PickUpTime = donation.PickUpTime,
                PrimaryImageUrl = donation.PrimaryImageUrl,
                Quantity = donation.Quantity,
                Status = donation.Status,
                DonationId = donation.Id,
                UserId = donation.UserId,
                RecipientId = (Guid)donation.Recipient,
            };
        }

        public async Task<BaseResponse<ICollection<DonationResponseCommandModel>>> ExpiredDonationByUser()
        {
            var user = await _user.LoggedInUser();
            var donation = await _donationRepo.GetDonationByUserAsync(d => d.UserId == user.Id);
            var donationType = donation.Where(t => t.Status == Domain.Enum.DonationStatus.Expired);
            if (donationType == null)
            {
                return new BaseResponse<ICollection<DonationResponseCommandModel>>
                {
                    Data = null,
                    IsSuccessfull = false,
                    Message = "No donation Created yet. create a survey now"
                };
            }
            return new BaseResponse<ICollection<DonationResponseCommandModel>>
            {
                IsSuccessfull = true,
                Message = "List of Donations",
                Data = donationType.Select(s => new DonationResponseCommandModel
                {
                    DonationImages = s.Images,
                    ExpirationDate = s.ExpirationDate,
                    FoodDetails = s.FoodDetails,
                    PickUpLocation = s.PickUpLocation,
                    PickUpTime = s.PickUpTime,
                    Quantity = s.Quantity,
                    Status = s.Status,
                    PrimaryImageUrl = s.PrimaryImageUrl,
                }).ToList()
            };
        }

    }
}

