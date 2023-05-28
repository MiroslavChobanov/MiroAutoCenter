using MiroAutoCenter.Core.Models.Ratings;

namespace MiroAutoCenter.Core.Contracts
{
    public interface IRatingService
    {
        public Guid AddRating(int starRating,
            string comment,
            string userId,
            DateTime ratingDate
            );
        public List<RatingsListingModel> All();
    }
}
