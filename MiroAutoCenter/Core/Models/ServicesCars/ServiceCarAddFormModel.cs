using MiroAutoCenter.Core.Models.Cars;
using MiroAutoCenter.Data.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MiroAutoCenter.Core.Models.ServicesCars
{
    public class ServiceCarAddFormModel
    {
        public Guid CarId { get; set; }
        public Guid ServiceId { get; set; }
        [Required(ErrorMessage = "{0} is a required field.")]
        public DateTime Time { get; set; }

        public IEnumerable<CarsListingModel> UserCars { get; set; }
    }
}
