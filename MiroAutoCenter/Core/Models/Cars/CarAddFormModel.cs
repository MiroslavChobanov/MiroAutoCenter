using MiroAutoCenter.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace MiroAutoCenter.Core.Models.Cars
{
    public class CarAddFormModel
    {
        [Required(ErrorMessage = "{0} is a required field.")]
        [StringLength(25, ErrorMessage = "{0} should be shorter than {1} symbols.")]
        [Display(Name = "Make")]
        public string Make { get; set; }

        [Required(ErrorMessage = "{0} is a required field.")]
        [StringLength(25, ErrorMessage = "{0} should be shorter than {1} symbols.")]
        [Display(Name = "Model")]
        public string Model { get; set; }

        [Required(ErrorMessage = "{0} is a required field.")]
        [Range(1900, 2100, ErrorMessage = "{0} can be between {1} and {2}")]
        [Display(Name = "Year Of Creation")]
        public int YearOfCreation { get; set; }

        [Required(ErrorMessage = "{0} is a required field.")]
        [StringLength(10, ErrorMessage = "{0} should be shorter than {1} symbols.")]
        [Display(Name = "Plate Number")]
        public string PlateNumber { get; set; }

        [Required(ErrorMessage = "{0} is a required field.")]
        [Range(0, 500000, ErrorMessage = "{0} can be between {1} and {2}")]
        [Display(Name = "Mileage")]
        public int Mileage { get; set; }
        [Required(ErrorMessage = "{0} is a required field.")]
        [Display(Name = "Car Type")]
        public Guid CarTypeId { get; set; }
        [Required]
        public IEnumerable<CarCarTypeViewModel> CarTypes { get; set; }

    }
}
