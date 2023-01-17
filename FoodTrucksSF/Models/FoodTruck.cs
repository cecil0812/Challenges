using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodTrucksSF
{
    public class FoodTruck
    {
        public FoodTruck()
        {
        }

        public FoodTruck(string name, double lat, double longitude)
        {
            this.Name = name;
            this.Latitude = lat;
            this.Longitude = longitude;
        }

        public string Name { get; }

        public double Latitude { get; }

        public double Longitude { get; }

        public override string ToString()
        {
            return Name;
        }
    }
}
