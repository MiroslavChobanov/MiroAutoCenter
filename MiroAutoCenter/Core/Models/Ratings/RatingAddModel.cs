using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace MiroAutoCenter.Core.Models.Ratings
{
    public class RatingAddModel
    {
        [Required(ErrorMessage = "{0} is a required field.")]
        [Range(1, 5, ErrorMessage = "{0} can be between {1} and {2}")]
        [Display(Name = "Рейтинг")]
        public int StarRating { get; set; }
        [StringLength(2000, ErrorMessage = "{0} should be shorter than {1} symbols.")]
        [MinLength(10, ErrorMessage = "{0} should be minimum {1} symbols.")]
        [Display(Name = "Коментар")]
        public string? Comment { get; set; }
    }
}
