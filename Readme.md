This repo contains two projects: FoodTrucksSF and FoodTruckFinder.

FoodTrucksSF is the endpoint which can calculated distances from a given lat/long to available foodtrucks.

FoodTruckFinder is a commandline application that calls the API and returns the results.
	FoodTruckFinder <lat> <long> <optional number to return>
	FoodTruckFinder retrieves the closest 5 trucks unless the number to return is specified.

To build FoodTrucksSF, all you have to do is load the project into Visual Studio and it should build as-is.

FoodTruckFinder requires a Nuget package: Microsoft.AspNet.WebApi.Client

You can install this from Visual Studio or you can use Powershell: Install-Package Microsoft.AspNet.WebApi.Client

After this, run FoodTrucksSF to get a local IIS started and then you can use the command line tool to make queries.
