using MiroAutoCenter.Core.Contracts;
using MiroAutoCenter.Core.Models.Admin;
using MiroAutoCenter.Data;
using MiroAutoCenter.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiroAutoCenter.Core.Services
{
    public class AdminService : IAdminService
    {
        private readonly MiroAutoCenterDbContext data;

        public AdminService(MiroAutoCenterDbContext data)
        {
            this.data = data;
        }
    }
}
