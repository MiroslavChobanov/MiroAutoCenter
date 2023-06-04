using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace MiroAutoCenter.Core.Models.Ratings
{
    public class RatingAddModel
    {
        [Required(ErrorMessage = "{0} е задължително поле.")]
        [Range(1, 5, ErrorMessage = "{0} може да е между {1} и {2}")]
        [Display(Name = "Рейтинг")]
        public int StarRating { get; set; }
        [StringLength(2000, ErrorMessage = "{0} трябва да е по-малко от {1} символа.")]
        [MinLength(10, ErrorMessage = "{0} трябва да е минимум {1} символа.")]
        [Display(Name = "Коментар")]
        public string? Comment { get; set; }
    }
}
