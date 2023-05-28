using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiroAutoCenter.Data.Models
{
    public class Rating
    {
        public Rating()
        {
            this.Id = Guid.NewGuid();
        }

        [Key]
        [Required]
        public Guid Id { get; set; }

        public int StarRating { get; set; }
        public string? Comment { get; set; }

        [Required]
        public string UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public WebsiteUser User { get; set; }

        public DateTime RatingDate { get; set; }
    }
}
