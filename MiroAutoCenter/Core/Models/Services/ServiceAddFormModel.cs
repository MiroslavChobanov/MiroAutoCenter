using MiroAutoCenter.Core.Models.Cars;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace MiroAutoCenter.Core.Models.Services
{
    public class ServiceAddFormModel
    {
        [Required(ErrorMessage = "{0} is a required field.")]
        [StringLength(50, ErrorMessage = "{0} should be shorter than {1} symbols.")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [StringLength(200, ErrorMessage = "{0} should be shorter than {1} symbols.")]
        [Display(Name = "Description")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "{0} is a required field.")]
        [Range(1, 5000, ErrorMessage = "{0} can be between {1} and {2}")]
        [Display(Name = "Price")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "{0} is a required field.")]
        [StringLength(20, ErrorMessage = "{0} should be shorter than {1} symbols.")]
        [Display(Name = "Time")]
        public string Time { get; set; }

    }
}
