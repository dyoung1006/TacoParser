using System;
using System.Linq;
using System.IO;

namespace LoggingKata
{
    class Program
    {
        static readonly ILog logger = new TacoLogger();
        const string csvPath = "TacoBell-US-AL.csv";

        static void Main(string[] args)
        {
            #region Instructions
            // TODO:  Find the two Taco Bells that are the furthest from one another.
            // HINT:  You'll need two nested forloops ---------------------------
            // use File.ReadAllLines(path) to grab all the lines from your csv file

            // Log and error if you get 0 lines and a warning if you get 1 line

            // Create a new instance of your TacoParser class

            // Grab an IEnumerable of locations using the Select command: var locations = lines.Select(parser.Parse);

            // DON'T FORGET TO LOG YOUR STEPS

            // Now that your Parse method is completed, START BELOW ----------

            // TODO: Create two `ITrackable` variables with initial values of `null`. These will be used to store your two taco bells that are the farthest from each other.
            // Create a `double` variable to store the distance

            // Include the Geolocation toolbox, so you can compare locations: `using GeoCoordinatePortable;`

            //HINT NESTED LOOPS SECTION---------------------
            // Do a loop for your locations to grab each location as the origin (perhaps: `locA`)

            // Create a new corA Coordinate with your locA's lat and long

            // Now, do another loop on the locations with the scope of your first loop, so you can grab the "destination" location (perhaps: `locB`)

            // Create a new Coordinate with your locB's lat and long

            // Now, compare the two using `.GetDistanceTo()`, which returns a double
            // If the distance is greater than the currently saved distance, update the distance and the two `ITrackable` variables you set above

            // Once you've looped through everything, you've found the two Taco Bells farthest away from each other.            
            #endregion

            logger.LogInfo("Log initialized");                        
            var lines = File.ReadAllLines(csvPath);

            switch (lines.Count())
            { 
                case 0:
                    Exception e = new Exception();
                    logger.LogError($"The file has zero records",e);
                    logger.LogInfo($"program can not continue without enough data to run.");
                    break;
                case 1:
                    logger.LogWarning($"file may have only 1 line.");
                    logger.LogInfo($"program can not determine distance with one reference point.");
                    break;
                default:
                    logger.LogInfo($"Lines: {lines.Count()} in file.");
                    ITrackable tacoBellA = null;
                    ITrackable tacoBellB = null;
                    double distanceBetween = 0;
                    TacoParser.RunParseProgram(lines,out tacoBellA,out tacoBellB, out distanceBetween, logger);
                    logger.LogInfo($"{tacoBellA.Name} and {tacoBellB.Name} are the furthest apart, with a distance of {distanceBetween} meters");
                    break;                    
            }

            Console.ReadLine();            
        }
    }
}
