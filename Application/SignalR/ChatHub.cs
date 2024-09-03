

using Application.Features.DTOs;
using Application.Features.Interfaces.IRepositries;
using Domain.Entities;
using Microsoft.AspNetCore.SignalR;

namespace Application.SignalR
{
    public class ChatHub : Hub
    {
        private readonly IMessageRepository _messageRepo;
        private readonly IDonationRepository _donationRepo;
        private readonly IHubContext<NotificationHub> _hubContext;
        private readonly ConnectionMappingService _connectionMappingService;

       

        public ChatHub(IMessageRepository messageRepo, IDonationRepository donationRepo, IHubContext<NotificationHub> hubContext, ConnectionMappingService connectionMappingService)
        {
            _messageRepo = messageRepo;
            _donationRepo = donationRepo;
            _hubContext = hubContext;
            _connectionMappingService = connectionMappingService;
        }

       


        public async Task InitializeConnection(Guid donationId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, donationId.ToString());
        }


        public async Task SendMessage(CreateMessageCommandModel request)
        {
            var donation = await _donationRepo.Get(d => d.Id == request.donationId);

            var message = new Message
            {
                DonationId = request.donationId,
                Content = request.content,
                SentAt = DateTime.UtcNow,
                UserId = request.UserId,
                RecipientId = (Guid)donation.Recipient,
                DonorId = request.donationId,
            };

            _messageRepo.Add(message);
            _messageRepo.Save();

            var messageData = new
            {
                Message = message.Content,
                Timestamp = message.SentAt.ToString("g"),
                UserId = request.UserId,
            };

            await Clients.Group(donation.Id.ToString()).SendAsync("ReceiveMessage", messageData);
        }
       
    }
}
