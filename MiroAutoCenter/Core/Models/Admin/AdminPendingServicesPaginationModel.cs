namespace MiroAutoCenter.Core.Models.Admin
{
    public class AdminPendingServicesPaginationModel
    {
        public int PageNo { get; set; }

        public int TotalRecords { get; set; }

        public int PageSize { get; set; }

        public List<AdminAllPendingServicesModel> AllPendingServices { get; set; }
    }
}
