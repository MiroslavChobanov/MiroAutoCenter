using MiroAutoCenter.Core.Models.Services;

namespace MiroAutoCenter.Core.Contracts
{
    public interface IServiceService
    {
        public List<ServicesListingModel> All();
        public Guid CreateService(
           string name,
           string description,
           decimal price,
           string time
           );

        public ServicesListingModel Details(Guid id);

        public bool Edit(
            Guid id,
            string name,
            string description,
            decimal price,
            string time
           );

        public bool Delete(Guid id, bool isDeleted);

        public ServiceDeleteModel DeleteViewData(Guid id);

        public ServiceAddFormModel EditViewData(Guid id);

        public Guid CreateAppointment(Guid serviceId, Guid carId, DateTime time);

        public ServicesPaginationModel AllServices(int pageNo, int pageSize);
    }
}
