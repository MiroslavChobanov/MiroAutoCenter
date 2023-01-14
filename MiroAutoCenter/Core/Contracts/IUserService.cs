using MiroAutoCenter.Core.Models.Cars;
using MiroAutoCenter.Core.Models.Users;
using MiroAutoCenter.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiroAutoCenter.Core.Contracts
{
    public interface IUserService
    {
        public string IdByUser(string userId);

        public UserDetailsModel UserDetails(string id);

        public IEnumerable<AppointmentModel> UserApproved(string userId);
        public IEnumerable<AppointmentModel> UserRejected(string userId);
        public IEnumerable<AppointmentModel> UserWaiting(string userId);

    }
}
