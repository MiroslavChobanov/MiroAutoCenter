using MiroAutoCenter.Core.Contracts;

namespace MiroAutoCenter.Core.Models.Admin
{
    public class AdminSidebarInfoModel
    {
        private readonly IAdminService admins;
        public AdminSidebarInfoModel(IAdminService admins)
        {
            this.admins = admins;
        }
    }
}
