using MiroAutoCenter.Core.Models.Cars;
using MiroAutoCenter.Data.Models;

namespace MiroAutoCenter.Core.Contracts
{
    public interface ICarService
    {
        //public List<CarsListingModel> All();


        public Guid CreateCar(
            string make,
            string model,
            int yearOfCreation,
            bool isDeleted,
            string plateNumber,
            int mileage,
            Guid carTypeId,
            string userId
            );

        IEnumerable<CarCarTypeViewModel> AllCarTypes();
        public CarDetailsModel Details(Guid id);

        public bool Edit(
            Guid id,
           string make,
            string model,
            int yearOfCreation,
            string plateNumber,
            int mileage,
            Guid carTypeId
           );

        public bool Delete(Guid id, bool isDeleted);

        public CarDeleteModel DeleteViewData(Guid id);

        public CarAddFormModel EditViewData(Guid id);

        IEnumerable<CarsListingModel> ByUser(string userId);

        public Guid IdByUser(string userId);

        public bool EditStatus(Guid id, Guid? statusId);
        public CarStatusEditModel EditStatusViewData(Guid id);

        public IEnumerable<CarStatusEditModel> GetCarsWithStatus();

        public IEnumerable<CarStatusViewModel> AllCarStatuses();

        public IEnumerable<CarStatusEditModel> GetReadyCars(IQueryable<Car> carQuery);

        public IEnumerable<CarStatusEditModel> ReadyCarsByUser(string userId);
    }
}
