using Application.Features.DTOs;
using Application.Features.Interfaces.IRepositries;
using Application.Features.Interfaces.IServices;


namespace Application.Features.Queries.Get
{
    public class DonationReview(IFoodCollectionRepository _review, IDonationRepository _donationRepo, ICurrentUser _user, IUserRepository _userRepo) : IDonationReview
    {

        public async Task<BaseResponse<ICollection<DonationReviewModel>>> AllUserReview()
        {
            var loggedInUser = await _user.LoggedInUser();

            var userDonations = await _donationRepo.GetDonationByUserAsync(d => d.UserId == loggedInUser.Id);

            if (userDonations == null || !userDonations.Any())
            {
                return new BaseResponse<ICollection<DonationReviewModel>>
                {
                    Data = new List<DonationReviewModel>(),
                    IsSuccessfull = false,
                };
            }

            var reviews = await _review.GetAll();
            var filteredReviews = reviews.Where(r => userDonations.Select(d => d.Id).Contains(r.DonationId)).ToList();

            if (!filteredReviews.Any())
            {
                return new BaseResponse<ICollection<DonationReviewModel>>
                {
                    Data = new List<DonationReviewModel>(),
                    IsSuccessfull = false,
                };
            }

            var reviewModels = new List<DonationReviewModel>();

            foreach (var r in filteredReviews)
            {
                var user = await _userRepo.Get(u => u.Id == r.Recipient);
                var reviewModel = new DonationReviewModel
                {
                    Review = r.Review,
                    FoodDetails = r.Donation.FoodDetails,
                    Quantity = r.Donation.Quantity,
                    ExpirationDate = r.Donation.ExpirationDate,
                    PickUpLocation = r.Donation.PickUpLocation,
                    PrimaryImageUrl = r.Donation.Images,
                    RecipientEmail = user.Email,
                    RecipientName = user.Name,
                };

                reviewModels.Add(reviewModel);
            }

            return new BaseResponse<ICollection<DonationReviewModel>>
            {
                IsSuccessfull = true,
                Data = reviewModels
            };
        }

        public async Task<BaseResponse<ICollection<DonationReviewModel>>> GetAllInappropriateReviews()
        {
            var inappropriateWords = new List<string> { "bad", "spoil", "damage", "poor", "awful", "terrible", "smell" , "stingy", "wicked"};

            var allReviews = await _review.GetAll();

            var inappropriateReviews = allReviews
                .Where(r => ContainsInappropriateWords(r.Review, inappropriateWords))
                .ToList();

            if (!inappropriateReviews.Any())
            {
                return new BaseResponse<ICollection<DonationReviewModel>>
                {
                    Data = new List<DonationReviewModel>(),
                    IsSuccessfull = false,
                    Message = "No inappropriate reviews found."
                };
            }

            var review = new List<DonationReviewModel>();
            foreach(var r in inappropriateReviews)
            {
                var user = await _userRepo.Get(u => u.Id == r.UserId);
                var reviewModel = new DonationReviewModel
                {
                    Review = r.Review,
                    FoodDetails = r.Donation.FoodDetails,
                    Quantity = r.Donation.Quantity,
                    ExpirationDate = r.Donation.ExpirationDate,
                    PickUpLocation = r.Donation.PickUpLocation,
                    PrimaryImageUrl = r.Donation.Images,
                    RecipientEmail = user.Email,
                    RecipientName = user.Name,
                };

                review.Add(reviewModel);
            }
            return new BaseResponse<ICollection<DonationReviewModel>>
            {
                IsSuccessfull = true,
                Data = review
            };
        }

        private bool ContainsInappropriateWords(string? review, List<string> inappropriateWords)
        {
            if (string.IsNullOrEmpty(review))
                return false;

            return inappropriateWords.Any(word => review.IndexOf(word, StringComparison.OrdinalIgnoreCase) >= 0);
        }


    }
}
