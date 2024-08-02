using Application.Features.Interfaces.IRepositries;
using Domain.Entities;
using Infrastructure.persistence.Context;


namespace Infrastructure.persistence.Repository.Implementation
{
    public class MessageRepository : GenericRepository<Message>, IMessageRepository
    {
        public MessageRepository(FoodConnectDB context) : base(context)
        {
        }
    }
}
