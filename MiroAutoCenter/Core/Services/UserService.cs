using MiroAutoCenter.Core.Contracts;
using MiroAutoCenter.Core.Models.Users;
using MiroAutoCenter.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiroAutoCenter.Core.Services
{
    public class UserService : IUserService
    {
        private readonly MiroAutoCenterDbContext data;

        public UserService(MiroAutoCenterDbContext data)
        {
            this.data = data;
        }

        public string IdByUser(string userId)
        {
            return this.data.WebsiteUsers
                .Where(x => x.Id == userId)
                .Select(x => x.Id)
                .First();
        }

        public UserDetailsModel UserDetails(string id)
        {
            return this.data.WebsiteUsers
                .Where(x => x.Id == id)
                .Select(x => new UserDetailsModel
                {
                    Id = id,
                    Email = x.Email,
                    Username = x.UserName,
                    Role = this.data.Roles.First(x => x.Id == this.data.UserRoles.First(x => x.UserId == id).RoleId).Name
                })
                .First();
        }

    }
}
