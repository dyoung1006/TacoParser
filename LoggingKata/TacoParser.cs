using System;
using System.Data;
using System.Linq;
using GeoCoordinatePortable;

namespace LoggingKata
{
    /// <summary>
    /// Parses a POI file to locate all the Taco Bells
    /// </summary>
    public class TacoParser
    {
        static readonly ILog logger = new TacoLogger();
        
        public ITrackable Parse(string line)
        {
            #region  Instructions
            //logger.LogInfo("Begin parsing");

            // Take your line and use line.Split(',') to split it up into an array of strings, separated by the char ','

            // If your array.Length is less than 3, something went wrong

            // Log that and return null

            // Do not fail if one record parsing fails, return null
            
            // grab the latitude from your array at index 0
            // grab the longitude from your array at index 1
            // grab the name from your array at index 2

            // Your going to need to parse your string as a `double`
            // which is similar to parsing a string as an `int`

            // You'll need to create a TacoBell class
            // that conforms to ITrackable

            // Then, you'll need an instance of the TacoBell class
            // With the name and point set correctly

            // Then, return the instance of your TacoBell class
            // Since it conforms to ITrackable
            #endregion

            var cells = line.Split(',');

            double? latitude = null;
            double? longitude = null;
            string storeName = null;

            switch (cells.Length)
            {
                case 3:
                    try
                    { 
                        latitude = double.Parse(cells[0]);
                    }
                    catch 
                    {                        
                        logger.LogWarning($"{line} - couldnt parse latitude");                        
                    }
                    
                    try
                    {
                        longitude = double.Parse(cells[1]);
                    }
                    catch
                    {                        
                        logger.LogWarning($"{line} - couldnt parse longitude");
                    }
                    try 
                    {
                        storeName = cells[2];
                    } 
                    catch 
                    {                        
                        logger.LogWarning($"{line} - couldnt parse storename");
                    }

                    while (latitude != null && longitude != null && storeName != null)
                    {
                        var tacoBell = new TacoBell();
                        tacoBell.Name = storeName;
                        tacoBell.Location = new Point { Latitude = (double)latitude, Longitude = (double)longitude };
                        logger.LogInfo("Row Parsed");
                        return tacoBell;
                    }
                    break;
                default:
                    logger.LogWarning($"{line} - doesnt have 3 columns/cells...");
                    break;
            }
            return null; 
        }

        public static void RunParseProgram(string[] _lines, out ITrackable _tacoBellA, out ITrackable _tacoBellB, out double _distanceBetween, ILog _logger)
        {
            var parser = new TacoParser();

            _logger.LogInfo("Begin parsing");
            var locations = _lines.Select(parser.Parse).Where( x => x!= null).ToArray();
            _tacoBellA = null;
            _tacoBellB = null;
            double distance = 0;

            foreach (var locationA in locations)
            {
                GeoCoordinate a = new GeoCoordinate { Latitude = locationA.Location.Latitude, Longitude = locationA.Location.Longitude };

                foreach (var locationB in locations)
                {
                    GeoCoordinate b = new GeoCoordinate { Latitude = locationB.Location.Latitude, Longitude = locationB.Location.Longitude };

                    if (a.GetDistanceTo(b) > distance)
                    {
                        distance = a.GetDistanceTo(b);
                        _tacoBellA = locationA;
                        _tacoBellB = locationB;
                    }
                }
            }

            _ = _tacoBellA;
            _ = _tacoBellB;
            _distanceBetween = distance;
        }
    }
}