using Application.Features.DTOs;
using Application.Features.Interfaces.IRepositries;
using Application.Features.Interfaces.IServices;
using Domain.Entities;
using Microsoft.Extensions.Logging;
using Org.BouncyCastle.Cms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Queries.Get
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _messageRepo;
        private readonly ICurrentUser _user;
        private readonly IDonationRepository _donationRepo;
        private readonly IFoodCollectionRepository _foodCollectionRepo;
        public MessageService(IMessageRepository messageRepo, IDonationRepository donationRepo, IFoodCollectionRepository foodCollectionRepo, ICurrentUser user) 
        {  _messageRepo = messageRepo;
            _donationRepo = donationRepo;
            _foodCollectionRepo = foodCollectionRepo;
            _user = user;
        }
        public async Task<BaseResponse<MessageCommandModel>> GetMessagesAsync(Guid userId, Guid recipientId)
        {
            var message = await _messageRepo.GetAll();
            var userMessage = message.Where(m => m.DonorId == userId && m.RecipientId == recipientId).FirstOrDefault();
            if (userMessage == null)
            {
                return new BaseResponse<MessageCommandModel>
                {
                    IsSuccessfull = false,
                };
            }
            return new BaseResponse<MessageCommandModel>
            {
                IsSuccessfull = true,
                Data = new MessageCommandModel
                {
                    DonorId = userMessage.DonorId,
                    Content = userMessage.Content,
                    DonationId = userMessage.DonationId,
                    RecipientId = recipientId,
                    SentAt = userMessage.SentAt,
                    UserId = userMessage.UserId
                }   
            };
        }

        public async Task<ICollection<MessageCommandModel>> GetMessagesByDonationIdAsync(Guid donationId)
        {
            var messages = await _messageRepo.GetAll(); 

            var filteredMessages = messages.Where(m => m.DonationId == donationId).ToList();

            if (filteredMessages == null || !filteredMessages.Any())
            {
                return new List<MessageCommandModel>();
            }

            return filteredMessages.Select(s => new MessageCommandModel
            {
                DonorId = s.DonorId,
                Content = s.Content,
                DonationId = s.DonationId,
                RecipientId = s.RecipientId,
                SentAt = s.SentAt,
                UserId = s.UserId
            }).ToList();
        }


        public async Task<bool> DeleteMessagesByDonationIdAsync(Guid donationId)
        {
            var messages = await _messageRepo.GetAll();
            messages.Where(m => m.DonationId == donationId);
            if (messages != null)
            {
                foreach (var message in messages)
                {
                    _messageRepo.Delete(message);
                }
                _messageRepo.Save();
                return true;
            }
            return false;
        }

        public async Task<bool> MarkMessageAsReceivedAsync(Guid donationId)
        {
            var message = await _messageRepo.Get(m => m.DonationId == donationId);
            if (message != null)
            {
                _messageRepo.Update(message);
                _messageRepo.Save(); 
                return true;
            }
            return false;
        }

        public async Task<BaseResponse<string>> DonationReceived(Guid donationId, string review)
        {
            var loggedInUser = await _user.LoggedInUser();
            var donation = await _donationRepo.Get(d => d.Id == donationId);
            if(donation.Recipient != loggedInUser.Id)
            {
                return new BaseResponse<string>
                {
                    IsSuccessfull = false,
                    Message = "Donor already Changed the Status to Available "
                };
            }
            
            donation.Status = Domain.Enum.DonationStatus.Received;
            _donationRepo.Update(donation);

            await DeleteMessagesByDonationIdAsync(donationId);

            var collection = new FoodCollection
            {
                DonationId = donationId,
                Recipient = (Guid)donation.Recipient,
                Review = review,
                UserId = donation.UserId
            };

            _foodCollectionRepo.Add(collection);
            _donationRepo.Save();

            return new BaseResponse<string>
            {
                IsSuccessfull = true,
                Message = "Donation Received Successfully \n Please do ensure to leave a review "
            };
        }


        

        public async Task<bool> ChangeDonationStatusAsync(Guid donationId, string reason)
        {
            var donation = await _donationRepo.Get(d => d.Id == donationId);
            if (donation.Status == Domain.Enum.DonationStatus.Received)
            {
                return false;
            }
            donation.Status = Domain.Enum.DonationStatus.Available;
            donation.Recipient = null;
            _donationRepo.Update(donation);

            await DeleteMessagesByDonationIdAsync(donationId);

            var collection = new FoodCollection
            {
                DonationId = donationId,
                Recipient = (Guid)donation.Recipient,
                Review = reason,
                UserId = donation.UserId
            };

            _foodCollectionRepo.Add(collection);
            _donationRepo.Save();
            return true;

        }

    }
}
