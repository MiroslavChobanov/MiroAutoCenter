using MiroAutoCenter.Core.Models.Cars;

namespace MiroAutoCenter.Core.Contracts
{
    public interface ICarService
    {
        public List<CarsListingModel> All();


        public Guid CreateCar(
            string make,
            string model,
            int yearOfCreation,
            bool isDeleted,
            string plateNumber,
            int mileage,
            Guid carTypeId
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
    }
}
