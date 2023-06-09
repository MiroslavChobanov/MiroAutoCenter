using System.ComponentModel.DataAnnotations;

namespace MiroAutoCenter.Data.Models
{
    public class CarStatus
    {
        public CarStatus()
        {
            this.Id = Guid.NewGuid();
            this.Cars = new HashSet<Car>();
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

        public ICollection<Car> Cars { get; set; }
    }
}
