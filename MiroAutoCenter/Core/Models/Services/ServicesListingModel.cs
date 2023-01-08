using MiroAutoCenter.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace MiroAutoCenter.Core.Models.Services
{
    public class ServicesListingModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Time { get; set; }

    }
}
