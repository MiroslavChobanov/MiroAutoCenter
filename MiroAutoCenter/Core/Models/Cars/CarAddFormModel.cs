using MiroAutoCenter.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace MiroAutoCenter.Core.Models.Cars
{
    public class CarAddFormModel
    {
        [Required(ErrorMessage = "{0} е задължително поле.")]
        [StringLength(25, ErrorMessage = "{0} трябва да е по-малко от {1} символа.")]
        [MinLength(2, ErrorMessage = "{0} трябва да е минимум {1} символа.")]
        [Display(Name = "Марка")]
        public string Make { get; set; }

        [Required(ErrorMessage = "{0} е задължително поле.")]
        [StringLength(25, ErrorMessage = "{0} трябва да е по-малко от {1} символа.")]
        [MinLength(2, ErrorMessage = "{0} трябва да е минимум {1} символа.")]
        [Display(Name = "Модел")]
        public string Model { get; set; }

        [Required(ErrorMessage = "{0} е задължително поле.")]
        [Range(1900, 2100, ErrorMessage = "{0} може да е между {1} и {2}")]
        [Display(Name = "Година на производство")]
        public int YearOfCreation { get; set; }

        [Required(ErrorMessage = "{0} е задължително поле.")]
        [StringLength(10, ErrorMessage = "{0} трябва да е по-малко от {1} символа.")]
        [MinLength(4, ErrorMessage = "{0} трябва да е минимум {1} символа.")]
        [Display(Name = "Регистрационен номер")]
        public string PlateNumber { get; set; }

        [Required(ErrorMessage = "{0} е задължително поле.")]
        [Range(0, 500000, ErrorMessage = "{0} може да е между {1} и {2}")]
        [Display(Name = "Пробег")]
        public int Mileage { get; set; }
        [Required(ErrorMessage = "{0} е задължително поле.")]
        [Display(Name = "Категория")]
        public Guid CarTypeId { get; set; }
        [Required]
        public IEnumerable<CarCarTypeViewModel> CarTypes { get; set; }

    }
}
