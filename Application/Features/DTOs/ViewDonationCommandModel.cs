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
        public record DonationCommand : IRequest<PagedResponse<ICollection<DonationResponseCommandModel>>>
        {
            public int DonationType {  get; set; } 
            public PaginationFilter filter { get; set; }    
        }

        public record DonationResponseCommandModel
        {
            public string FoodDetails { get; set; } = default!;
            public int Quantity { get; set; }
            public DateTime ExpirationDate { get; set; }
            public DonationStatus Status { get; set; }
            public DateTime PickUpTime { get; set; }
            public string PickUpLocation { get; set; } = default!;
            public ICollection<string> DonationImages { get; set; } = null!;
            public string PrimaryImageUrl { get; set; } = null!;
            public string? DonationMadeBy { get; set; }
            public string? UserEmail { get; set; }
            public string? UserRole { get; set; }
            public string? Name { get; set; }
            public string? PhoneNumber { get; set; }
            public string? profileImage { get; set; }
            public string? Address { get; set; }
            public string? ReasonForDisapproval { get; set; }
            public string? ManagerName { get; set; }
            public string? ManagerPhoneNumber { get; set; }
            public string? ManagerProfileImage { get; set; }
            public string? ManagerAddress { get; set; }
        }

        public record UserDonatedResponse
        {
            
        }
    }
}
