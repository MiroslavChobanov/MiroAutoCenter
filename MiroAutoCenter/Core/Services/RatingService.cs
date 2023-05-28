using MiroAutoCenter.Core.Contracts;
using MiroAutoCenter.Core.Models.Ratings;
using MiroAutoCenter.Core.Models.Services;
using MiroAutoCenter.Data;
using MiroAutoCenter.Data.Models;

namespace MiroAutoCenter.Core.Services
{
    public class RatingService : IRatingService
    {
        private readonly MiroAutoCenterDbContext data;

        public RatingService(MiroAutoCenterDbContext data)
        {
            this.data = data;
        }
        public Guid AddRating(int starRating,
            string comment,
            string userId,
            DateTime ratingDate
            )
        {
            var newRating = new Rating
            {
                StarRating = starRating,
                Comment = comment,
                UserId = userId,
                RatingDate = ratingDate
            };

            this.data.Ratings.Add(newRating);
            this.data.SaveChanges();

            return newRating.Id;
        }

        public List<RatingsListingModel> All()
        {
            return this.data
                 .Ratings
                 .Select(x => new RatingsListingModel
                 {
                     Id = x.Id,
                     StarRating = x.StarRating,
                     Comment = x.Comment,
                     Username = x.User.UserName,
                     RatingDate = x.RatingDate
                 })
                 .OrderBy(x => x.Id)
                 .ToList();
        }
    }
}
