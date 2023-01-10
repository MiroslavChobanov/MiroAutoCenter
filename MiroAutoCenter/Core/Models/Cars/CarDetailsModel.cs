﻿using MiroAutoCenter.Data.Models;


namespace MiroAutoCenter.Core.Models.Cars
{
    public class CarDetailsModel
    {
        public Guid Id { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int YearOfCreation { get; set; }
        public bool IsDeleted { get; set; }
        public string PlateNumber { get; set; }
        public int Mileage { get; set; }
    }
}
