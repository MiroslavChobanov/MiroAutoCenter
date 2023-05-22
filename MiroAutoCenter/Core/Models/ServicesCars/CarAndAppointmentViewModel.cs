using MiroAutoCenter.Core.Models.Cars;
using System.Collections.Generic;

namespace MiroAutoCenter.Core.Models.ServicesCars
{
    public class CarAndAppointmentViewModel
    {
        public IEnumerable<CarsListingModel> UserCars { get; set; }
        public List<AppointmentViewModel> AvailableDays { get; set; }
        public Guid CarId { get; set; }
    }
}
