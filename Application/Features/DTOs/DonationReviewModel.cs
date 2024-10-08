﻿
namespace Application.Features.DTOs
{
    public class DonationReviewModel
    {
        public string? Review { get; set; }
        public string FoodDetails { get; set; } = default!;
        public int Quantity { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string PickUpLocation { get; set; } = default!;
        public string PrimaryImageUrl { get; set; } = default!;
        public string RecipientName { get; set; } 
        public string RecipientEmail { get; set; } 
    }
}
