using Domain.Entities;
using MediatR;


namespace Application.Features.DTOs
{
    public class CreateMessageCommandModel : IRequest<BaseResponse<Message>>
    {
      
        public Guid donationId { get; set; }
        public Guid UserId { get; set; }
        public string content { get; set; }
       
    }
}
