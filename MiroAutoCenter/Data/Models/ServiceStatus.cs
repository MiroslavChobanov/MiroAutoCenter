using System.ComponentModel.DataAnnotations;

namespace MiroAutoCenter.Data.Models
{
    public class ServiceStatus
    {
        public ServiceStatus()
        {
            this.Id = Guid.NewGuid();
            this.ServicesCars = new HashSet<ServiceCar>();
        }

        [Key]
        [Required]
        public Guid Id { get; set; }

        [Required]
        [StringLength(50)]
        public string StatusDescription { get; set; }

        [Required]
        [StringLength(50)]
        public string ClassColor { get; set; }

        [Required]
        [Range(0, 10)]
        public int Counter { get; set; }

        public ICollection<ServiceCar> ServicesCars { get; set; }
    }
}
