using Application.Features.DTOs;


namespace Application.Features.Interfaces.IServices
{
    public interface IMessageService
    {
        Task<BaseResponse<MessageCommandModel>> GetMessagesAsync(Guid userId, Guid recipientId);
        Task<ICollection<MessageCommandModel>> GetMessagesByDonationIdAsync(Guid donationId);
        Task<bool> MarkMessageAsReceivedAsync(Guid donationId);
        Task<bool> DeleteMessagesByDonationIdAsync(Guid donationId);
        Task<bool> ChangeDonationStatusAsync(Guid donationId, string reason);
        Task<BaseResponse<string>> DonationReceived(Guid donationId, string review);
        
    }
}
