using MiroAutoCenter.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace MiroAutoCenter.Core.Models.Cars
{
    public class CarStatusEditModel
    {
        public Guid Id { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int YearOfCreation { get; set; }
        public bool IsDeleted { get; set; }
        public string PlateNumber { get; set; }
        public int Mileage { get; set; }

        public Guid CarTypeId { get; set; }
        [Required]
        public CarType CarType { get; set; }

        [Display(Name = "Статус")]
        public Guid? CarStatusId { get; set; }
        public CarStatus? CarStatus { get; set; }
        [Required]
        public IEnumerable<CarStatusViewModel> CarStatuses { get; set; }
    }
}
