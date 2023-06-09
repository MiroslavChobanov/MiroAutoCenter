
using MiroAutoCenter.Core.Models.Admin;
using MiroAutoCenter.Core.Models.Cars;
using MiroAutoCenter.Core.Models.Users;

namespace MiroAutoCenter.Core.Contracts
{
    public interface IAdminService
    {
        public bool Approve(Guid id);
        public bool Disapprove(Guid id);

        public AdminPendingServicesPaginationModel AllPendingServices(int pageNo, int pageSize);
        public IEnumerable<AppointmentModel> GetAllApproved();
        public IEnumerable<CarStatusEditModel> GetReceivedCars();
    }
}
