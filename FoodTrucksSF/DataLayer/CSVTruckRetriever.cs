using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodTrucksSF.DataLayer
{
    public class CSVTruckRetriever : ITruckRetriever
    {
        private string filename = @"Mobile_Food_Facility_Permit.csv";

        public CSVTruckRetriever()
        {
        }

        public List<FoodTruck> getFoodTrucks()
        {
            List<FoodTruck> trucks = new List<FoodTruck>();

            using (var reader = new System.IO.StreamReader(filename))
            {
                int lineNumber = 0;
                string line = "";
                while ((line = reader.ReadLine()) != null)
                {
                    if (lineNumber != 0)
                    {
                        var values = line.Split(',');

                        if (values[10] == "APPROVED")
                        {
                            trucks.Add(new FoodTruck(values[1], Double.Parse(values[14]), Double.Parse(values[15])));
                        }
                    }

                    ++lineNumber;
                }
            }

            return trucks;
        }
    }
}
