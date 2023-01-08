using Microsoft.AspNetCore.Identity;

namespace MiroAutoCenter.Data.Models
{
    public class WebsiteUser : IdentityUser
    {
        public DateTime RegisteredOn { get; set; }
    }
}
