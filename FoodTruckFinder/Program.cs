using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace FoodTruckFinder
{
    class Program
    {
        //37.794331&longitude=-122.3958111
        private const string URL = "https://localhost:44330/api/foodtruck";

        static void Main(string[] args)
        {
            if (args.Length == 0 || args.Length == 1)
            {
                Console.WriteLine("Usage:");
                Console.WriteLine("FoodTruckFinder <lat> <long>");
                return;
            }

            string urlParameters = getUrlParamsFromCmdLine(args);

            Console.WriteLine("Searching for food trucks near to " + args[0] + ", " + args[1]);

            HttpClient client = new HttpClient();
            client.Timeout = new TimeSpan(0, 0, 30);
            client.BaseAddress = new Uri(URL);

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage resp = client.GetAsync(urlParameters).Result;
            if (resp.IsSuccessStatusCode)
            {
                Console.WriteLine("Trucks found, ordered from nearest to furthest:");

                var allTrucks = resp.Content.ReadAsAsync<IEnumerable<string>>().Result;

                foreach (var name in allTrucks)
                {
                    Console.WriteLine("{0}", name);
                }
            }
            else
            {
                Console.WriteLine("Error returned from the server: {0} ({1})", (int)resp.StatusCode, resp.ReasonPhrase);
            }

            client.Dispose();
        }

        private static string getUrlParamsFromCmdLine(string[] args)
        {
            StringBuilder urlParameters = new StringBuilder("?latitude=" + args[0] + "&longitude=" + args[1]);

            if (args.Length == 3)
            {
                urlParameters.Append("&number=" + args[2]);
            }

            return urlParameters.ToString();
        }
    }
}
