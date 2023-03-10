
using MiroAutoCenter.Core.Models.Admin;

namespace MiroAutoCenter.Core.Contracts
{
    public interface IAdminService
    {
        public bool Approve(Guid id);
        public bool Disapprove(Guid id);

        public AdminPendingServicesPaginationModel AllPendingServices(int pageNo, int pageSize);
    }
}
