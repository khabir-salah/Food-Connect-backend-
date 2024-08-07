using Domain.Entities;
using MediatR;


namespace Application.Features.DTOs
{
    public class CreateMessageCommandModel : IRequest<BaseResponse<Message>>
    {
        public Guid DonorId { get; set; }
        public Guid RecipientId { get; set; }
        public Guid DonationId { get; set; }
        public string? Content { get; set; }
        public DateTime SentAt { get; set; }
        public bool IsRead { get; set; } = false;
    }
}
