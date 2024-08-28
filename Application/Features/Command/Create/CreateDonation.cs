using Application.Features.DTOs;
using Application.Features.Interfaces.IRepositries;
using Application.Features.Interfaces.IServices;
using Domain.Constant;
using Domain.Entities;
using Domain.Enum;
using Google.Api;
using Google.Api.Gax.Grpc.Rest;
using Google.Cloud.Vision.V1;
using MediatR;
using Microsoft.AspNetCore.Http;


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
                string primaryImageUrl1 = await SaveFileAsync(request.DonationImages);
                
                var user = await _currentUser.LoggedInUser();
                
                var donation = new Donation
                {
                    Quantity = request.Quantity,
                    PickUpTime = request.PickUpTime,
                    ExpirationDate = request.ExpirationDate,
                    FoodDetails = request.FoodDetails,
                    PrimaryImageUrl = primaryImageUrl,
                    Images = primaryImageUrl1,
                    PickUpLocation = request.PickUpLocation,
                    Status = DonationStatus.pending,
                    UserId =  user.Id,
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
                var filePath = Path.Combine("wwwroot", uploadDir);

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
                //var client = ImageAnnotatorClient.Create();
                var client = new ImageAnnotatorClientBuilder
                {
                    GrpcAdapter = RestGrpcAdapter.Default
                }.Build();
                var image = Image.FromUri(imageUrl);
                var response = await client.DetectLabelsAsync(image);
                return response;
            }
        }
    }
}
