using Microsoft.EntityFrameworkCore;
using MiroAutoCenter.Core.Contracts;
using MiroAutoCenter.Core.Models.Users;
using MiroAutoCenter.Data;
using MiroAutoCenter.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiroAutoCenter.Core.Services
{
    public class UserRoleService : IUserRoleService
    {
        private readonly MiroAutoCenterDbContext data;

        public UserRoleService(MiroAutoCenterDbContext data)
        {
            this.data = data;
        }

        public async Task<WebsiteUser> GetUserById(string id)
        {
            return await data.FindAsync<WebsiteUser>(id);
        }

        public async Task<UserEditModel> GetUserForEdit(string id)
        {
            var user = await data.FindAsync<WebsiteUser>(id);

            return new UserEditModel()
            {
                Id = user.Id,
                Username = user.UserName,
                Email = user.Email,
            };
        }

        public async Task<IEnumerable<UserListingModel>> GetUsers()
        {
            return await data.WebsiteUsers
                .Select(x => new UserListingModel()
                {
                    Id = x.Id,
                    Username = x.UserName,
                    Email = x.Email,
                })
                .ToListAsync();
        }

        public async Task<bool> UpdateUser(UserEditModel model)
        {
            bool result = false;
            var user = await data.FindAsync<WebsiteUser>(model.Id);

            if (user != null)
            {
                user.UserName = model.Username;

                await data.SaveChangesAsync();
                result = true;
            }

            return result;
        }
    }
}
