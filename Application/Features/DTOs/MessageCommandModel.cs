using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Application.Features.DTOs.ViewDonationCommandModel;

namespace Application.Features.DTOs
{
    public class MessageCommandModel
    {
        public Guid DonorId { get; set; }
        public Guid RecipientId { get; set; }
        public Guid DonationId { get; set; }
        public string? Content { get; set; }
        public DateTime SentAt { get; set; } 
        public bool IsRead { get; set; }
    }

    public class DonationWithMessagesViewModel
    {
        public DonationResponseCommandModel Donation {  get; set; }
        public ICollection<MessageCommandModel> Messages {  get; set; }
    }
}
