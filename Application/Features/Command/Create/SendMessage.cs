using Application.Features.DTOs;
using Application.Features.Interfaces.IRepositries;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Command.Create
{
    public class SendMessage
    {
        public class Handler : IRequestHandler<CreateMessageCommandModel, BaseResponse<Message>>
        {
            private readonly IMessageRepository _messageRepo;
            private readonly IDonationRepository _donationRepo;
            public Handler(IMessageRepository messageRepo, IDonationRepository donationRepo)
            {
                _messageRepo = messageRepo;
                _donationRepo = donationRepo;
            }
            public async Task<BaseResponse<Message>> Handle(CreateMessageCommandModel request, CancellationToken cancellationToken)
            {
                var donation = await _donationRepo.Get(d => d.Id == request.DonationId);
                var message = new Message
                {
                    Content = request.Content,
                    DonationId = donation.Id,
                    DonorId = donation.UserId,
                    IsRead = false,
                    RecipientId = donation.Recipient
                };
                _messageRepo.Add(message);
                _messageRepo.Save();
                return new BaseResponse<Message>
                {
                    IsSuccessfull = true,
                    Data =  message
                };
            }
        }
    }
}
