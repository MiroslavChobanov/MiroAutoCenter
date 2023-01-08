using System.ComponentModel.DataAnnotations;

namespace MiroAutoCenter.Data.Models
{
    public class Car
    {
        public Car()
        {
            this.Id = Guid.NewGuid();
            this.Services = new HashSet<Service>();
        }

        [Key]
        [Required]
        public Guid Id { get; set; }

        [Required]
        [StringLength(25)]
        public string Make { get; set; }

        [Required]
        [StringLength(25)]
        public string Model { get; set; }

        [Required]
        [Range(1900, 2100)]
        public int YearOfCreation { get; set; }

        [Required]
        public bool IsDeleted { get; set; }
        [StringLength(10)]
        public string PlateNumber { get; set; }
        [Required]
        [Range(0, 500000)]
        public int Mileage { get; set; }

        public Guid CarTypeId { get; set; }
        public CarType CarType { get; set; }

        public IEnumerable<Service> Services { get; init; }
    }
}
