using MiroAutoCenter.Core.Contracts;
using MiroAutoCenter.Core.Models.Cars;
using MiroAutoCenter.Data;
using MiroAutoCenter.Data.Models;


namespace MiroAutoCenter.Core.Services
{
    public class CarService : ICarService
    {
        private readonly MiroAutoCenterDbContext data;

        public CarService(MiroAutoCenterDbContext data)
        {
            this.data = data;
        }

        public List<CarsListingModel> All()
        {
            return this.data
                 .Cars
                 .Where(x => !x.IsDeleted)
                 .Select(x => new CarsListingModel
                 {
                     Id = x.Id,
                     Make = x.Make,
                     Model = x.Model,
                     YearOfCreation = x.YearOfCreation,
                     PlateNumber= x.PlateNumber,
                     Mileage= x.Mileage,
                     CarTypeId= x.CarTypeId,
                     CarType = x.CarType
                     
                 })
                 .OrderBy(x => x.Id)
                 .ToList();
        }

        public IEnumerable<CarCarTypeViewModel> AllCarTypes()
        {
            return this.data
                .CarTypes
                .Select(x => new CarCarTypeViewModel
                {
                    Id = x.Id,
                    Type = x.Type
                })
                .ToList();
        }

        public Guid CreateCar(string make,
            string model,
            int yearOfCreation,
            bool isDeleted,
            string plateNumber,
            int mileage,
            Guid carTypeId)
        {
            var newCar = new Car
            {
                Make = make,
                Model = model,
                YearOfCreation = yearOfCreation,
                PlateNumber = plateNumber,
                Mileage = mileage,
                IsDeleted = isDeleted,
                CarTypeId = carTypeId
            };

            this.data.Cars.Add(newCar);
            this.data.SaveChanges();

            return newCar.Id;
        }

        public CarDetailsModel Details(Guid id)
        {
            return this.data.Cars
                .Where(x => x.Id == id)
                .Select(x => new CarDetailsModel
                {
                    Id = x.Id,
                    Make = x.Make,
                    Model = x.Model,
                    YearOfCreation = x.YearOfCreation,
                    PlateNumber = x.PlateNumber,
                    Mileage = x.Mileage,
                    CarTypeId = x.CarTypeId
                })
                .First();
        }

        public bool Edit(Guid id,
            string make,
            string model,
            int yearOfCreation,
            string plateNumber,
            int mileage,
            Guid carTypeId)
        {
            var carData = this.data.Cars.Find(id);

            if (carData == null)
            {
                return false;
            }

            carData.Make = make;
            carData.Model = model;
            carData.YearOfCreation = yearOfCreation;
            carData.PlateNumber = plateNumber;
            carData.Mileage = mileage;
            carData.CarTypeId = carTypeId;


            this.data.SaveChanges();

            return true;
        }

        public bool Delete(Guid id, bool isDeleted)
        {
            var carData = this.data.Cars.Find(id);

            if (carData == null)
            {
                return false;
            }

            carData.IsDeleted = isDeleted;

            this.data.SaveChanges();

            return true;
        }

        public CarDeleteModel DeleteViewData(Guid id)
        {
            return this.data.Cars
                .Where(x => x.Id == id)
                .Select(x => new CarDeleteModel
                {
                    Make = x.Make,
                    Model = x.Model,
                    PlateNumber = x.PlateNumber,
                    YearOfCreation = x.YearOfCreation
                })
                .First();
        }

        public CarAddFormModel EditViewData(Guid id)
        {
            return this.data.Cars
                .Where(x => x.Id == id)
                .Select(x => new CarAddFormModel
                {
                    Make = x.Make,
                    Model = x.Model,
                    PlateNumber = x.PlateNumber,
                    YearOfCreation = x.YearOfCreation, 
                    Mileage = x.Mileage,
                    CarTypeId = x.CarTypeId
                })
                .First();
        }
    }
}
