using CodingChallenge.Models;
using GeoCoordinatePortable;

namespace CodingChallenge
{
    public static class Compute
    {
        public static double Distance(Location lastLocation, Location currentLocation)
        {
            var last = new GeoCoordinate(lastLocation.Lat, lastLocation.Long);
            var current = new GeoCoordinate(currentLocation.Lat, currentLocation.Long);

            return last.GetDistanceTo(current);
        }

        public static double Speed(long lastTime, long currentTime, double distance)
        {
            var diffSeconds = (currentTime - lastTime) / 1000;
            var kph = (distance / 1000.0f) / (diffSeconds / 3600.0f);
            var mph = kph / 1.609f;

            return mph;
        }
    }
}
