using MiroAutoCenter.Data.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MiroAutoCenter.Core.Models.Ratings
{
    public class RatingsListingModel
    {
        public Guid Id { get; set; }

        public int StarRating { get; set; }
        public string Comment { get; set; }

        public string Username { get; set; }

        public DateTime RatingDate { get; set; }
    }
}
