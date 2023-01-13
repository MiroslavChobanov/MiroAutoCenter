using MiroAutoCenter.Core.Models.Admin;

namespace MiroAutoCenter.Core.Models.Services
{
    public class ServicesPaginationModel
    {
        public int PageNo { get; set; }

        public int TotalRecords { get; set; }

        public int PageSize { get; set; }

        public List<ServicesListingModel> AllServices { get; set; }
    }
}
