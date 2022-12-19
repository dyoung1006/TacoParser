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

            if (cells.Length < 3)
            {       
                logger.LogWarning($"{line} - doesnt have 3 columns/cells...");
                return null; // TODO Implement
            }
                       
            double latitude = double.Parse(cells[0]);
            double longitude = double.Parse(cells[1]);
            string storeName = cells[2];
                       
            var tacoBell = new TacoBell();
            tacoBell.Name = storeName;
            tacoBell.Location = new Point { Latitude = latitude, Longitude = longitude };

            logger.LogInfo("Row Parsed");
            return tacoBell;
        }

        public static void RunParseProgram(string[] _lines, out ITrackable _tacoBellA, out ITrackable _tacoBellB, out double _distanceBetween, ILog _logger)
        {
            var parser = new TacoParser();

            _logger.LogInfo("Begin parsing");
            var locations = _lines.Select(parser.Parse).ToArray();

            _tacoBellA = null;
            _tacoBellB = null;
            double distance = 0;

            //for (int i = 0; i < locations.Length; i++)
            //{
            //    var locA = locations[i];

            //    var corA = new GeoCoordinate();
            //    corA.Latitude = locA.Location.Latitude;
            //    corA.Longitude = locA.Location.Longitude;

            //    for (int j = 0; j < locations.Length; j++)
            //    {
            //        var locB = locations[j];

            //        var corB = new GeoCoordinate();
            //        corB.Latitude = locB.Location.Latitude;
            //        corB.Longitude = locB.Location.Longitude;

            //        if (corA.GetDistanceTo(corB) > distance)
            //        {
            //            _tacoBellA = locA;
            //            _tacoBellB = locB;
            //        }
            //    }
            //}

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

            //GeoCoordinate a = new GeoCoordinate { Latitude = _tacoBellA.Location.Latitude, Longitude = _tacoBellB.Location.Longitude };
            //GeoCoordinate b = new GeoCoordinate { Latitude = _tacoBellB.Location.Latitude, Longitude = _tacoBellB.Location.Longitude };
            _tacoBellA = _tacoBellA;
            _tacoBellB = _tacoBellB;
            _distanceBetween = distance;

            logger.LogInfo($"{_tacoBellA.Name} and {_tacoBellB.Name} are the furthest apart, with a distance of {_distanceBetween} meters");
        }
    }
}