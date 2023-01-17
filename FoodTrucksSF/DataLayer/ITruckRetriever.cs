using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodTrucksSF.DataLayer
{
    interface ITruckRetriever
    {
        List<FoodTruck> getFoodTrucks();
    }
}
