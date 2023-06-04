using MiroAutoCenter.Core.Models.Cars;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace MiroAutoCenter.Core.Models.Services
{
    public class ServiceAddFormModel
    {
        [Required(ErrorMessage = "{0} е задължително поле.")]
        [StringLength(50, ErrorMessage = "{0} трябва да е по-малко от {1} символа.")]
        [MinLength(5,ErrorMessage = "{0} трябва да е минимум {1} символа.")]
        [Display(Name = "Име")]
        public string Name { get; set; }

        [StringLength(200, ErrorMessage = "{0} трябва да е по-малко от {1} символа.")]
        [MinLength(20, ErrorMessage = "{0} трябва да е минимум {1} символа.")]
        [Display(Name = "Описание")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "{0} е задължително поле.")]
        [Range(1, 5000, ErrorMessage = "{0} може да е между {1} и {2}")]
        [Display(Name = "Цена")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "{0} е задължително поле.")]
        [StringLength(20, ErrorMessage = "{0} трябва да е по-малко от {1} символа.")]
        [Display(Name = "Времетраене")]
        public string Time { get; set; }

    }
}
