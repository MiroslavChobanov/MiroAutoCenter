using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MiroAutoCenter.Data.Models
{
    public class ServiceCar
    {
        public Guid CarId { get; set; }
        public Car Car { get; set; }
        public Guid ServiceId { get; set; }
        public Service Service { get; set; }

        [Required]
        public Guid ServiceStatusId { get; set; }

        [ForeignKey(nameof(ServiceStatusId))]
        public ServiceStatus ServiceStatus { get; set; }
    }
}
