using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeoCoordinatePortable;

namespace LoggingKata
{
    public static class Helpers
    {
        public static double DistanceBetween(GeoCoordinate a, GeoCoordinate b)
        {
            return a.GetDistanceTo(b);
        }
    }
}
