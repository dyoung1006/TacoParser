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
