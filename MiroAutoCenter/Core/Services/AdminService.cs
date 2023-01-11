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

        public AdminPendingServicesPaginationModel AllPendingServices(int pageNo, int pageSize)
        {
            AdminPendingServicesPaginationModel result = new AdminPendingServicesPaginationModel()
            {
                PageNo = pageNo,
                PageSize = pageSize
            };

            result.TotalRecords = this.data.ServicesCars.Where(x => x.ServiceStatus.ClassColor == "table-warning").Count();
            result.AllPendingServices = this.data.ServicesCars
                .Where(x => x.ServiceStatus.ClassColor == "table-warning")
                .Select(x => new AdminAllPendingServicesModel
                {
                    Id = x.Id,
                    Time= x.Time
                })
                 .Skip(pageNo * pageSize - pageSize)
                 .Take(pageSize)
                 .ToList();

            return result;
        }

        public bool Approve(Guid id)
        {
            var serviceCarData = this.data.ServicesCars.First(x => x.Id == id);

            if (serviceCarData == null)
            {
                return false;
            }

            serviceCarData.ServiceStatusId = this.data.ServiceStatuses.First(x => x.ClassColor == "table-success").Id;
            serviceCarData.ServiceStatus = this.data.ServiceStatuses.First(x => x.ClassColor == "table-success");

            this.data.SaveChanges();

            return true;
        }

        public bool Disapprove(Guid id)
        {
            var serviceCarData = this.data.ServicesCars.First(x => x.Id == id);

            if (serviceCarData == null)
            {
                return false;
            }

            serviceCarData.ServiceStatusId = this.data.ServiceStatuses.First(x => x.ClassColor == "table-danger").Id;
            serviceCarData.ServiceStatus = this.data.ServiceStatuses.First(x => x.ClassColor == "table-danger");

            this.data.SaveChanges();

            return true;
        }
    }
}
