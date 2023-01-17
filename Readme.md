This repo contains two projects: FoodTrucksSF and FoodTruckFinder.

FoodTrucksSF is the endpoint which can calculated distances from a given lat/long to available foodtrucks.

FoodTruckFinder is a commandline application that calls the API and returns the results.
	FoodTruckFinder <lat> <long> <optional number to return>
	FoodTruckFinder retrieves the closest 5 trucks unless the number to return is specified.

To build FoodTrucksSF, all you have to do is load the project into Visual Studio and it should build as-is.

FoodTruckFinder requires a Nuget package: Microsoft.AspNet.WebApi.Client

You can install this from Visual Studio or you can use Powershell: Install-Package Microsoft.AspNet.WebApi.Client

After this, run FoodTrucksSF to get a local IIS started and then you can use the command line tool to make queries.


Some notes on the projects:

FoodTrucksSF:
- Due to time constraints, I decided to just read the CSV file for the food truck data.
- Since there aren't that many trucks, they are all read in but if there were a large amount I might try partitioning the data in some method so that only a subset would need to be searched.  With my current solution, you could also read in the file and process each entry as you read it, rather than reading the entire file into memory.
	- Additionally, if some sort of database was used, certain portions could be indexed to make the seaching faster.
	- I think a good solution if there were a large amount of entries would be to partition the lat/longs into groupings.  Depending on how fine grain it would  need to be, you could partition it out and then figure out which section your current lat/long fall into and only search that section.
		- If one did this, they would probably want to set up a backend process that would take in the feed and process it into the partitions for later use.  You could also set the frequency for updating depending on how often the feed changes.
- I did add an Interface that I felt would be useful to swap out databases like that.
- I didn't really have a DTO model but I do have Datamodels
- The calculateDistance() method is mostly pulled from the internet as to how to calculate distance on a globe between two coords.  This is probably overkill for such a small radius but it was easy to implement.
- One improvement in the return values would be to create a bigger array that has the actual distances to the various trucks that were picked.

FoodTruckFinder:
- This is pretty straight forward.  I am a back end engineer so I went with the command line.  If I had an extra hour or two, I could have made a GUI application.

