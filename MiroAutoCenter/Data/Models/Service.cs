using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MiroAutoCenter.Data.Models
{
    public class Service
    {
        public Service()
        {
            this.Id = Guid.NewGuid();
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


        public Guid ServiceStatusId { get; set; }

        [ForeignKey(nameof(ServiceStatusId))]
        public ServiceStatus ServiceStatus { get; set; }


        public string UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public WebsiteUser User { get; set; }


        public Guid CarId { get; set; }

        [ForeignKey(nameof(CarId))]
        public Car Car { get; set; }

        public bool IsApproved { get; set; }
    }
}
