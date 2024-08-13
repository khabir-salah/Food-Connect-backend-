using Application.Features.DTOs;


namespace Application.Features.Interfaces.IServices
{
    public interface IMessageService
    {
        Task<BaseResponse<MessageCommandModel>> GetMessagesAsync(Guid userId, Guid recipientId);
        Task<MessageCommandModel> GetMessagesByDonationIdAsync(Guid donationId);
        Task<bool> MarkAsReceivedAsync(Guid donationId);
        Task<bool> DeleteMessagesByDonationIdAsync(Guid donationId);
    }
}
