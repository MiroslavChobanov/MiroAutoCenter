using Microsoft.AspNetCore.Identity;

namespace MiroAutoCenter.Data.Models
{
    public class WebsiteUser : IdentityUser
    {
        public DateTime RegisteredOn { get; set; }

        public WebsiteUser()
        {
            this.Cars = new HashSet<Car>();
        }

        public ICollection<Car> Cars { get; set; }
    }
}
