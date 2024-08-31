using Application.Features.DTOs;
using Application.Features.Interfaces.IRepositries;
using Application.Features.Interfaces.IServices;
using Application.SignalR;
using Domain.Constant;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Queries.Get
{
    public class DonationValidation : IDonationValidation
    {

        private readonly IDonationRepository _donationRepo;
        private readonly ICurrentUser _user;
        private readonly IHubContext<NotificationHub> _hubContext;

        public DonationValidation(IDonationRepository donationRepo,  ICurrentUser user, IHubContext<NotificationHub> hubContext)
        {
            _donationRepo = donationRepo;
            _user = user;
            _hubContext = hubContext;
        }
        public async Task<BaseResponse<string>> ApproveDonationByManager(Guid id)
        {
            var user = await _user.LoggedInUser();

            // Check if super admin
            if (user.Role.Name == RoleConst.Admin)
            {
                var donation = await _donationRepo.Get(d => d.Id == id);
                if (donation == null)
                {
                    return new BaseResponse<string>
                    {
                        IsSuccessfull = false,
                        Message = "Donation not found."
                    };
                }

                var newStatus = donation.Status = Domain.Enum.DonationStatus.Approve;

                _donationRepo.Update(donation);
                _donationRepo.Save();
                await _hubContext.Clients.User(user.Id.ToString()).SendAsync("ReceiveNotification", $"Your donation status has changed to: {newStatus}");

                return new BaseResponse<string>
                {
                    IsSuccessfull = true,
                    Message = "Donation Approved by super admin."
                };
            }
            else
            {
                // Regular manager
                var donation = await _donationRepo.Get(d => d.Id == id);
                if (donation == null)
                {
                    return new BaseResponse<string>
                    {
                        IsSuccessfull = false,
                        Message = "Donation not found."
                    };
                }

                donation.Status = Domain.Enum.DonationStatus.Approve;
                donation.ManagerId = user.Id;

                _donationRepo.Update(donation);
                _donationRepo.Save();

                return new BaseResponse<string>
                {
                    IsSuccessfull = true,
                    Message = "Donation Approved."
                };
            }
        }

        public async Task<BaseResponse<string>> DispproveDonation(DisapproveDonationModel request)
        {
            var user = await _user.LoggedInUser();

            // Check if super admin
            if (user.Role.Name == RoleConst.Admin)
            {
                var donation = await _donationRepo.Get(d => d.Id == request.Id);
                if (donation == null)
                {
                    return new BaseResponse<string>
                    {
                        IsSuccessfull = false,
                        Message = "Donation not found."
                    };
                }

                var newStatus = donation.Status = Domain.Enum.DonationStatus.Disapprove;
                donation.ReasonForDisapproval = request.ReasonForDisapproval;

                _donationRepo.Update(donation);
                _donationRepo.Save();
                await _hubContext.Clients.User(user.Id.ToString()).SendAsync("ReceiveNotification", $"Your donation status has changed to: {newStatus}");

                return new BaseResponse<string>
                {
                    IsSuccessfull = true,
                    Message = "Donation disapproved by super admin."
                };
            }
            else
            {
                // Regular manager
                var donation = await _donationRepo.Get(d => d.Id == request.Id);
                if (donation == null)
                {
                    return new BaseResponse<string>
                    {
                        IsSuccessfull = false,
                        Message = "Donation not found."
                    };
                }

                var newStatus = donation.Status = Domain.Enum.DonationStatus.Disapprove;
                donation.ManagerId = user.Id;
                donation.ReasonForDisapproval = request.ReasonForDisapproval;

                _donationRepo.Update(donation);
                _donationRepo.Save();
                await _hubContext.Clients.User(user.Id.ToString()).SendAsync("ReceiveNotification", $"Your donation status has changed to: {newStatus}");

                return new BaseResponse<string>
                {
                    IsSuccessfull = true,
                    Message = "Donation disapproved."
                };
            }
        }

        public async Task<BaseResponse<string>> ClaimDonation(Guid id)
        {
            var recipient = await _user.LoggedInUser();
            var donation = await _donationRepo.Get(d => d.Id == id);

            var lastClaim = await _donationRepo.GetLastClaimByUser(recipient.Id);
            if (lastClaim != null)
            {
                var timeSinceLastClaim = DateTime.UtcNow - lastClaim.LastClaimTime.Value;
                if (timeSinceLastClaim.TotalHours < 5)
                {
                    return new BaseResponse<string>
                    {
                        IsSuccessfull = false,
                    };
                }
            }

            donation.Recipient = recipient.Id;
            var newStatus = donation.Status = Domain.Enum.DonationStatus.Claim;
            donation.LastClaimTime = DateTime.UtcNow;
            _donationRepo.Update(donation);
            _donationRepo.Save();
            await _hubContext.Clients.User(recipient.Id.ToString()).SendAsync("ReceiveNotification", $"Your donation status has changed to: {newStatus}");
            return new BaseResponse<string>
            {
                IsSuccessfull = true,
                Message = "Donation Claimed Successfully \n Proceed to location to receive Donation "
            };
        }
    }
}
