using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiroAutoCenter.Core.Models.Cars
{
    public class CarDeleteModel
    {
        public string Make { get; set; }

        public string Model { get; set; }

        public string PlateNumber { get; set; }

        public int YearOfCreation { get; set; }
    }
}
