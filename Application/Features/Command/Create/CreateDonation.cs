using Application.Features.DTOs;
using Application.Features.Interfaces.IRepositries;
using Application.Features.Interfaces.IServices;
using Domain.Constant;
using Domain.Entities;
using Domain.Enum;
using Google.Cloud.Vision.V1;
using MediatR;
using Microsoft.AspNetCore.Http;

using static Application.Features.DTOs.CreateDonationCommandModel;

namespace Application.Features.Command.Create
{
    public class CreateDonation
    {
        //creating a donation
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

                // Analyze the primary image using Google Cloud Vision API
                var primaryImageLabels = await AnalyzeImageAsync(primaryImageUrl);

                // Optionally, analyze other donation images
                var donationImageLabels = new List<IReadOnlyList<EntityAnnotation>>();
                foreach (var imageUrl in imageUrls)
                {
                    var labels = await AnalyzeImageAsync(imageUrl);
                    donationImageLabels.Add(labels);
                }

                // Use the analysis results to determine the food safety and details
                // For example, check if the image contains certain labels
                bool isSafe = primaryImageLabels.Any(label => label.Description.Contains("safe food consumable good healthy"));

                // updating status base on result
                var status = isSafe ? DonationStatus.Available : DonationStatus.pending;

                DonationMadeBy donationMadeBy;
                if (user.Role.Name == RoleConst.FamilyHead && user.Family != null)
                {
                    donationMadeBy = DonationMadeBy.FamilyHead;
                }
                else if(user.Organisation != null)
                {
                    donationMadeBy = DonationMadeBy.Orgainization;
                }
                else
                {
                    donationMadeBy = DonationMadeBy.Individual;
                }

                var donation = new Donation
                {
                    Quantity = request.Quantity,
                    PickUpTime = request.PickUpTime,
                    ExpirationDate = request.ExpirationDate,
                    FoodDetails = request.FoodDetails,
                    PrimaryImageUrl = primaryImageUrl,
                    Images = imageUrls,
                    PickUpLocation = request.PickUpLocation,
                    Status = status,
                    UserId =  user.Id,
                    DonationMadeBy = donationMadeBy
                };
                _donationRepository.Add(donation);
                _donationRepository.Save();

                return new BaseResponse<string>
                {
                    IsSuccessfull = true,
                    Message = "Donation created successfully",
                };
            }


            // creating and saving the donaton image in folder 
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


           //using google cloud vison to scan food donation
            private async Task<IReadOnlyList<EntityAnnotation>> AnalyzeImageAsync(string imageUrl)
            {
                var client = ImageAnnotatorClient.Create();
                var image = Image.FromUri(imageUrl);
                var response = await client.DetectLabelsAsync(image);
                return response;
            }
        }
    }
}
