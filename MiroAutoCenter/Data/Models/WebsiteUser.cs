﻿using Microsoft.AspNetCore.Identity;

namespace MiroAutoCenter.Data.Models
{
    public class WebsiteUser : IdentityUser
    {
        public DateTime RegisteredOn { get; set; }

        public WebsiteUser()
        {
            this.Cars = new HashSet<Car>();
            this.Ratings = new HashSet<Rating>();
            this.Questions = new HashSet<Question>();
        }

        public ICollection<Car> Cars { get; set; }
        public ICollection<Rating> Ratings { get; set; }
        public ICollection<Question> Questions { get; set; }
    }
}
