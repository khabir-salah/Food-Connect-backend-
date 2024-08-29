using Application.Features.DTOs;
using Application.Features.Interfaces.IRepositries;
using Application.SignalR;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;


namespace Application.Features.Command.Create
{
    public class SendMessage
    {
        public class Handler : IRequestHandler<CreateMessageCommandModel, BaseResponse<Message>>
        {
            private readonly IMessageRepository _messageRepo;
            private readonly IDonationRepository _donationRepo;
            private readonly IHubContext<ChatHub> _hubContext;
            public Handler(IMessageRepository messageRepo, IDonationRepository donationRepo, IHubContext<ChatHub> hubContext)
            {
                _messageRepo = messageRepo;
                _donationRepo = donationRepo;
                _hubContext = hubContext;
            }
            public async Task<BaseResponse<Message>> Handle(CreateMessageCommandModel request, CancellationToken cancellationToken)
            {
                var donation = await _donationRepo.Get(d => d.Id == request.donationId);
                var message = new Message
                {
                    Content = request.content,
                    DonationId = donation.Id,
                    DonorId = donation.UserId,
                    IsRead = false,
                    RecipientId = (Guid)donation.Recipient
                };
                _messageRepo.Add(message);
                _messageRepo.Save();

                await _hubContext.Clients.User(message.RecipientId.ToString())
                .SendAsync("ReceiveMessage", new
                {
                    Message = message.Content,
                    DonationId = message.DonationId,
                    DonorId = message.DonorId,
                    IsRead = message.IsRead,
                    Timestamp = DateTime.UtcNow
                });
                return new BaseResponse<Message>
                {
                    IsSuccessfull = true,
                    Data = message
                };
            }
        }
    }
}
