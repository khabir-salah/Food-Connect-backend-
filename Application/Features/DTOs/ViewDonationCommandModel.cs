using Domain.Enum;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.DTOs
{
    public class ViewDonationCommandModel
    {
        public record DonationCommand : IRequest<BaseResponse<ICollection<DonationResponseCommandModel>>>
        {
            public int DonationType {  get; set; } 
        }

        public record DonationResponseCommandModel
        {
            public string FoodDetails { get; set; } = default!;
            public int Quantity { get; set; }
            public DateTime ExpirationDate { get; set; }
            public DonationStatus Status { get; set; }
            public DateTime PickUpTime { get; set; }
            public string PickUpLocation { get; set; } = default!;
            public IList<IFormFile> DonationImages { get; set; } = null!;
            public IFormFile PrimaryImageUrl { get; set; } = null!;
            public UserDonatedResponse UserDetails { get; set; } = null!;
            public string UserEmail { get; set; }
            public string UserRole { get; set; }
        }

        public record UserDonatedResponse
        {
            public string UserEmail { get; set; }
            public string UserRole { get; set; }
            public string FirstName { get; set; }
            public string PhoneNumber { get; set; }

        }
    }
}
