using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FoodTrucksSF.DataLayer;

namespace FoodTrucksSF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodTruckController : ControllerBase
    {
        const double RADIUS_OF_EARTH = 6371.392896;
        const int DEFAULT_TRUCKS_TO_RETURN = 5;

        // GET: api/FoodTruck
        [HttpGet]
        public IActionResult Get(double latitude, double longitude, int number)
        {
            ITruckRetriever retriever = new CSVTruckRetriever();

            if (number == 0)
            {
                number = DEFAULT_TRUCKS_TO_RETURN;
            }

            List<FoodTruck> trucks = retriever.getFoodTrucks();

            if (trucks.Count == 0)
            {
                return StatusCode(500);
            }

            List<string> closeTrucks = getCloseByTrucks(trucks, latitude, longitude, number);
            return Ok(closeTrucks);
        }

        // GET: api/FoodTruck/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/FoodTruck
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/FoodTruck/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        private List<string> getCloseByTrucks(List<FoodTruck> trucks, double userLatitude, double userLongitude, int numberToReturn)
        {
            List<string> validTrucks = new List<string>();
            List<FoodTruck> distancesToTrucks = new List<FoodTruck>();
            SortedDictionary<string, double> calculatedTrucks = new SortedDictionary<string, double>();

            foreach (FoodTruck truck in trucks)
            {
                double distance = calculateDistance(userLatitude, userLongitude, truck.Latitude, truck.Longitude);

                if (!calculatedTrucks.ContainsKey(truck.Name))
                {
                    calculatedTrucks.Add(truck.Name, distance);
                }
            }

            int count = 0;
            foreach (var pair in calculatedTrucks.OrderBy(p => p.Value))
            {
                if (count == numberToReturn)
                {
                    break;
                }

                validTrucks.Add(pair.Key);
                ++count;
            }

            return validTrucks;
        }

        private double calculateDistance(double lat1, double long1, double lat2, double long2)
        {
            double distance = 0.0;

            //Haversine formula from the internet
            //distance = R * 2 * aTan2 ( square root of A, square root of 1 - A )
            //                   where A = sinus squared (difference in latitude / 2) + (cosine of latitude 1 * cosine of latitude 2 * sinus squared (difference in longitude / 2))
            //                   and R = the circumference of the earth

            double differenceInLat = degreeToRadian(lat1 - lat2);
            double differenceInLong = degreeToRadian(long1 - long2);
            double aInnerFormula = Math.Cos(degreeToRadian(lat1)) * Math.Cos(degreeToRadian(lat2)) * Math.Sin(differenceInLong / 2) * Math.Sin(differenceInLong / 2);
            double aFormula = (Math.Sin((differenceInLat) / 2) * Math.Sin((differenceInLat) / 2)) + (aInnerFormula);
            distance = RADIUS_OF_EARTH * 2 * Math.Atan2(Math.Sqrt(aFormula), Math.Sqrt(1 - aFormula));

            return distance;
        }

        private double degreeToRadian(double coords)
        {
            return Math.PI * coords / 180.0;
        }
    }
}
