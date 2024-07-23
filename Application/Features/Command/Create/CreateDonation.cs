using Application.Features.DTOs;
using Application.Features.Interfaces.IRepositries;
using Application.Features.Interfaces.IServices;
using Domain.Entities;
using Domain.Enum;
using MediatR;
using Microsoft.AspNetCore.Http;

using static Application.Features.DTOs.CreateDonationCommandModel;

namespace Application.Features.Command.Create
{
    public class CreateDonation
    {
        
        public class Handler : IRequestHandler<CreateDonationCommand, BaseResponse<string>>
        {
            private readonly ICurrentUser _currentUser;
            private readonly IDonationRepository _donationRepository;
            public Handler(ICurrentUser currentUser, IDonationRepository donationRepository)
            {
                _currentUser = currentUser;
                _donationRepository = donationRepository;
            }
            public async Task<BaseResponse<string>> Handle(CreateDonationCommand request, CancellationToken cancellationToken)
            {
                string primaryImageUrl = await SaveFileAsync(request.PrimaryImageUrl);
                var imageUrls = new List<string>();

                if (request.DonationImages != null)
                {
                    foreach (var image in request.DonationImages)
                    {
                        string imageUrl = await SaveFileAsync(image);
                        imageUrls.Add(imageUrl);
                    }
                }
                var user = await _currentUser.LoggedInUser();

                var donation = new Donation
                {
                    Quantity = request.Quantity,
                    PickUpTime = request.PickUpTime,
                    ExpirationDate = request.ExpirationDate,
                    FoodDetails = request.FoodDetails,
                    PrimaryImageUrl = primaryImageUrl,
                    Images = imageUrls,
                    PickUpLocation = request.PickUpLocation,
                    Status = DonationStatus.pending,
                    UserId = user.Id
                };
                _donationRepository.Add(donation);
                _donationRepository.Save();

                return new BaseResponse<string>
                {
                    IsSuccessfull = true,
                    Message = "Donation created successfully",
                };
            }
            private async Task<string> SaveFileAsync(IFormFile file)
            {
                if (file == null || file.Length == 0)
                    return string.Empty;

                var uploadDir = "uploads";
                var filePath = Path.Combine("DonationImages", uploadDir);

                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }

                var uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                var fullPath = Path.Combine(filePath, uniqueFileName);

                using (var fileStream = new FileStream(fullPath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }

                return $"/{uploadDir}/{uniqueFileName}";
            }
        }


    }
}
