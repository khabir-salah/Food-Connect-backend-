﻿using Domain.Entities;
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
        public record CreateDonationCommand : IRequest<BaseResponse<string>>
        {
            public string FoodDetails { get; set; } = default!;
            public int Quantity { get; set; }
            public DateTime ExpirationDate { get; set; }
            public DateTime PickUpTime { get; set; }
            public string PickUpLocation { get; set; } = default!;
            public Guid UserId { get; set; }
            public IFormFile DonationImages { get; set; } 
            public IFormFile PrimaryImageUrl { get; set; } = null!;
        }

       


}
