using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MiroAutoCenter.Data.Models
{
    public class Service
    {
        public Service()
        {
            this.Id = Guid.NewGuid();
            this.ServicesCars = new HashSet<ServiceCar>();
        }

        [Key]
        [Required]
        public Guid Id { get; set; }

        [StringLength(50)]
        [Required]
        public string Name { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }

        public decimal Price { get; set; }

        [StringLength(50)]
        [Required]
        public string Time { get; set; }
        [Required]
        public bool IsDeleted { get; set; }

        [StringLength(500)]
        public string? UserMessage { get; set; }

        [StringLength(500)]
        public string? AdminMessage { get; set; }

        

        public bool IsApproved { get; set; }

        public IEnumerable<ServiceCar> ServicesCars { get; set; } 
    }
}
