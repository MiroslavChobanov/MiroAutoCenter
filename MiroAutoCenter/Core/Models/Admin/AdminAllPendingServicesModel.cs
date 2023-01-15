
namespace MiroAutoCenter.Core.Models.Admin
{
    public class AdminAllPendingServicesModel
    {
        public Guid Id { get; set; }

        public DateTime Time { get; set; }

        public string ServiceType { get; set; }
        public string CarMake { get; set; }
    }
}
