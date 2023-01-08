using System.ComponentModel.DataAnnotations;

namespace MiroAutoCenter.Data.Models
{
    public class CarType
    {
        public CarType()
        {
            this.Id = Guid.NewGuid();
            this.Cars = new HashSet<Car>();
        }

        [Key]
        [Required]
        public Guid Id { get; set; }
        [Required]
        [StringLength(25)]
        public string Type { get; set; }

        public IEnumerable<Car> Cars { get; set; }
    }
}
