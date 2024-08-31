using Application.Features.DTOs;
using Application.Features.Interfaces.IRepositries;
using Domain.Constant;
using Domain.Entities;
using Domain.Enum;
using Google.Type;
using MediatR;

using static Application.Features.DTOs.ViewDonationCommandModel;

namespace Application.Features.Queries.List
{
    public class ViewDonations
    {
        public class Handler : IRequestHandler<DonationCommand, PagedResponse<ICollection<DonationResponseCommandModel>>>
        {
            private readonly IUserRepository _userRepo;
            private readonly IDonationRepository _donationRepo;

            public Handler(IUserRepository userRepo, IDonationRepository donationRepo)
            {
                _userRepo = userRepo;
                _donationRepo = donationRepo;
            }
            public async Task<PagedResponse<ICollection<DonationResponseCommandModel>>> Handle(DonationCommand request, CancellationToken cancellationToken)
            {
                var getDonation = await _donationRepo.GetAllDonationByPage(request.filter);
                if (getDonation == null || !getDonation.Any())
                {
                    return new PagedResponse<ICollection<DonationResponseCommandModel>>(null, 0, 0, 0)
                    {
                        
                        IsSuccessfull = false,
                        Data = null ,
                        Message = "No Donation at the moment check back later"
                    };
                }

                

                switch(request.status)
                {
                    case 1:
                        {
                            var pending = getDonation.Where(d => d.Status == Domain.Enum.DonationStatus.pending);
                            if (pending.Any())
                            {

                                var pendingDonations = new List<DonationResponseCommandModel>();

                                foreach (var s in pending)
                                {
                                    var user = await _userRepo.GetUserAsync(u => u.Id == s.UserId);
                                    pendingDonations.Add(new DonationResponseCommandModel
                                    {
                                        FoodDetails = s.FoodDetails,
                                        Status = Domain.Enum.DonationStatus.pending,
                                        ExpirationDate = s.ExpirationDate,
                                        PickUpLocation = s.PickUpLocation,
                                        PickUpTime = s.PickUpTime,
                                        Quantity = s.Quantity,
                                        UserEmail = user.Email,
                                        UserRole = user.Role.Name,
                                        Address = user.Address,
                                        Name = user.Name,
                                        PhoneNumber = user.PhoneNumber,
                                        PrimaryImageUrl = s.PrimaryImageUrl,
                                        DonationImages = s.Images,
                                        DonationMadeBy = s.DonationMadeBy.ToString(),
                                        DonationId = s.Id,
                                    });
                                }


                                var totalRecords = await _donationRepo.CountAsync(Domain.Enum.DonationStatus.pending); 

                                return new PagedResponse<ICollection<DonationResponseCommandModel>>(pendingDonations, request.filter.PageNumber, request.filter.PageSize, totalRecords)
                                {
                                    IsSuccessfull = true,
                                    Message = "Pending Donations",
                                    Data = pendingDonations
                                };
                            }

                            return new PagedResponse<ICollection<DonationResponseCommandModel>>(null, 0, 0, 0)
                            {
                                IsSuccessfull = false,
                                Data = null,
                                Message = "No Donation at the moment check back later"
                            };
                        }
                    case 2:
                        {
                            var approve = getDonation.Where(u => u.Status == Domain.Enum.DonationStatus.Approve);
                            if (approve.Any())
                            {
                                var approveDonations = new List<DonationResponseCommandModel>();

                                foreach (var s in approve)
                                {
                                    var manager = await _userRepo.GetUserAsync(u => u.Id == s.ManagerId);
                                    var user = await _userRepo.GetUserAsync(u => u.Id == s.UserId);

                                    var donationResponse = new DonationResponseCommandModel
                                    {
                                        FoodDetails = s.FoodDetails,
                                        Status = Domain.Enum.DonationStatus.Approve,
                                        ExpirationDate = s.ExpirationDate,
                                        PickUpLocation = s.PickUpLocation,
                                        PickUpTime = s.PickUpTime,
                                        Quantity = s.Quantity,
                                        UserEmail = user.Email,
                                        UserRole = user.Role.Name,
                                        Address = user.Address,
                                        Name = user.Name,
                                        PhoneNumber = user.PhoneNumber,
                                        PrimaryImageUrl = s.PrimaryImageUrl,
                                        DonationImages = s.Images,
                                        DonationMadeBy = s.DonationMadeBy.ToString(),
                                        ManagerName = manager != null ? manager.Name : "Super Admin",
                                        ManagerEmail = manager != null ? manager.Email : "Approved By Super Admin",
                                        DonationId = s.Id,
                                    };

                                    approveDonations.Add(donationResponse);
                                }


                                var totalRecords = await _donationRepo.CountAsync(Domain.Enum.DonationStatus.Approve);

                                return new PagedResponse<ICollection<DonationResponseCommandModel>>(approveDonations, request.filter.PageNumber, request.filter.PageSize, totalRecords)
                                {
                                    IsSuccessfull = true,
                                    Message = "Apprroved Donations",
                                    Data = approveDonations
                                };
                            }
                            return new PagedResponse<ICollection<DonationResponseCommandModel>>(null, 0, 0, 0)
                            {
                                IsSuccessfull = false,
                                Data = null,
                                Message = "No Donation at the moment check back later"
                            };
                        }
                    case 3:
                        {
                            var disapprove = getDonation.Where(d => d.Status == Domain.Enum.DonationStatus.Disapprove);
                            if (disapprove.Any())
                            {
                                var disapproveDonations = new List<DonationResponseCommandModel>();

                                foreach (var s in disapprove)
                                {
                                    var manager = await _userRepo.GetUserAsync(u => u.Id == s.ManagerId);
                                    var user = await _userRepo.GetUserAsync(u => u.Id == s.UserId);

                                    var donationResponse = new DonationResponseCommandModel
                                    {
                                        FoodDetails = s.FoodDetails,
                                        Status = Domain.Enum.DonationStatus.Disapprove,
                                        ExpirationDate = s.ExpirationDate,
                                        PickUpLocation = s.PickUpLocation,
                                        PickUpTime = s.PickUpTime,
                                        Quantity = s.Quantity,
                                        UserEmail = user.Email,
                                        UserRole = user.Role.Name,
                                        Address = user.Address,
                                        Name = user.Name,
                                        PhoneNumber = user.PhoneNumber,
                                        PrimaryImageUrl = s.PrimaryImageUrl,
                                        DonationImages = s.Images,
                                        DonationMadeBy = s.DonationMadeBy.ToString(),
                                        ReasonForDisapproval = s.ReasonForDisapproval,
                                        ManagerName = manager != null ? manager.Name : "Super Admin",
                                        ManagerEmail = manager != null ? manager.Email : "Approved By Super Admin",
                                        DonationId = s.Id,
                                    };

                                    disapproveDonations.Add(donationResponse);
                                }

                                var totalRecords = await _donationRepo.CountAsync(Domain.Enum.DonationStatus.Disapprove);
                                

                                return new PagedResponse<ICollection<DonationResponseCommandModel>>(disapproveDonations, request.filter.PageNumber, request.filter.PageSize, totalRecords)
                                {
                                    IsSuccessfull = true,
                                    Message = "Disapproved Donations",
                                    Data = disapproveDonations
                                };
                            }
                            return new PagedResponse<ICollection<DonationResponseCommandModel>>(null, 0, 0, 0)
                            {
                                IsSuccessfull = false,
                                Data = null,
                                Message = "No Donation at the moment check back later"
                            };
                        }
                    case 4:
                        {
                            var received = getDonation.Where(d => d.Status == Domain.Enum.DonationStatus.Received);
                            if (received.Any())
                            {
                                var receiveDonations = await Task.WhenAll(received.Select(async s =>
                                {
                                    var user = await _userRepo.GetUserAsync(u => u.Id == s.UserId);
                                    var recipient = await _userRepo.GetUserAsync(u => u.Id == s.Recipient);
                                    return new DonationResponseCommandModel
                                    {
                                        FoodDetails = s.FoodDetails,
                                        Status = Domain.Enum.DonationStatus.Received,
                                        ExpirationDate = s.ExpirationDate,
                                        PickUpLocation = s.PickUpLocation,
                                        PickUpTime = s.PickUpTime,
                                        Quantity = s.Quantity,
                                        UserEmail = user.Email,
                                        UserRole = user.Role.Name,
                                        Address = user.Address,
                                        Name = user.Name,
                                        PhoneNumber = user.PhoneNumber,
                                        PrimaryImageUrl = s.PrimaryImageUrl,
                                        DonationImages = s.Images,
                                        DonationMadeBy = s.DonationMadeBy.ToString(),
                                        RecipientEmail = recipient.Email,
                                        RecipientName = recipient.Name,
                                        DonationId = s.Id,
                                    };
                                }).ToList());

                                var totalRecords = await _donationRepo.CountAsync(Domain.Enum.DonationStatus.Received);

                                return new PagedResponse<ICollection<DonationResponseCommandModel>>(receiveDonations, request.filter.PageNumber, request.filter.PageSize, totalRecords)
                                {
                                    IsSuccessfull = true,
                                    Message = "Received Donations",
                                    Data = receiveDonations
                                };
                            }
                            return new PagedResponse<ICollection<DonationResponseCommandModel>>(null, 0, 0, 0)
                            {
                                IsSuccessfull = false,
                                Data = null,
                                Message = "No Donation at the moment check back later"
                            };
                        }
                    case 5:
                        {
                            var claim = getDonation.Where(d => d.Status == Domain.Enum.DonationStatus.Claim);
                            if (claim.Any())
                            {
                                var claimedDonations = await Task.WhenAll(claim.Select(async s =>
                                {
                                    var user = await _userRepo.GetUserAsync(u => u.Id == s.UserId);
                                    return new DonationResponseCommandModel
                                    {
                                        FoodDetails = s.FoodDetails,
                                        Status = Domain.Enum.DonationStatus.Claim,
                                        ExpirationDate = s.ExpirationDate,
                                        PickUpLocation = s.PickUpLocation,
                                        PickUpTime = s.PickUpTime,
                                        Quantity = s.Quantity,
                                        UserEmail = user.Email,
                                        UserRole = user.Role.Name,
                                        Address = user.Address,
                                        Name = user.Name,
                                        PhoneNumber = user.PhoneNumber,
                                        PrimaryImageUrl = s.PrimaryImageUrl,
                                        DonationImages = s.Images,
                                        DonationMadeBy = s.DonationMadeBy.ToString(),
                                        DonationId = s.Id,
                                    };
                                }).ToList());

                                var totalRecords = await _donationRepo.CountAsync(Domain.Enum.DonationStatus.Claim);

                                return new PagedResponse<ICollection<DonationResponseCommandModel>>(claimedDonations, request.filter.PageNumber, request.filter.PageSize, totalRecords)
                                {
                                    IsSuccessfull = true,
                                    Message = "claimed Donations Donations",
                                    Data = claimedDonations
                                };
                            }
                            return new PagedResponse<ICollection<DonationResponseCommandModel>>(null, 0, 0, 0)
                            {
                                IsSuccessfull = false,
                                Data = null,
                                Message = "No Donation at the moment check back later"
                            };
                        }
                    case 6:
                        {
                            var available = getDonation.Where(d => d.Status == Domain.Enum.DonationStatus.Expired);
                            if (available.Any())
                            {
                                var availableDonations = await Task.WhenAll(available.Select(async s =>
                                {
                                    var user = await _userRepo.GetUserAsync(u => u.Id == s.UserId);
                                    return new DonationResponseCommandModel
                                    {
                                        FoodDetails = s.FoodDetails,
                                        Status = Domain.Enum.DonationStatus.Expired,
                                        ExpirationDate = s.ExpirationDate,
                                        PickUpLocation = s.PickUpLocation,
                                        PickUpTime = s.PickUpTime,
                                        Quantity = s.Quantity,
                                        UserEmail = user.Email,
                                        UserRole = user.Role.Name,
                                        Address = user.Address,
                                        Name = user.Name,
                                        PhoneNumber = user.PhoneNumber,
                                        PrimaryImageUrl = s.PrimaryImageUrl,
                                        DonationImages = s.Images,
                                        DonationMadeBy = s.DonationMadeBy.ToString(),
                                        DonationId = s.Id,
                                    };
                                }).ToList());

                            var totalRecords = await _donationRepo.CountAsync(Domain.Enum.DonationStatus.Expired);

                            return new PagedResponse<ICollection<DonationResponseCommandModel>>(availableDonations, request.filter.PageNumber, request.filter.PageSize, totalRecords)
                            {
                                IsSuccessfull = true,
                                Message = "available Donations",
                                Data = availableDonations
                            };
                        }
                        return new PagedResponse<ICollection<DonationResponseCommandModel>>(null, 0, 0, 0)
                        {
                            IsSuccessfull = false,
                            Data = null,
                            Message = "No Donation at the moment check back later"
                        };

                    }
                    default:
                        return new PagedResponse<ICollection<DonationResponseCommandModel>>(null, 0, 0, 0)
                        {
                            IsSuccessfull = false,
                            Data = null,
                            Message = "No Donation at the moment check back later"
                        };
                }
            }
        }
    }
}
