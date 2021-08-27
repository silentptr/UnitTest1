using System;

namespace UnitTest1.UI.Util
{
    public static class PlanetFilter
    {
        public static Predicate<Planet> ByName(string name)
        {
            return p => p.Name == name;
        }

        public static Predicate<Planet> ByDistanceFromSun(long distance)
        {
            return p => p.DistanceFromSun == distance;
        }

        public static Predicate<Planet> ByRadius(double radius)
        {
            return p => p.Radius == radius;
        }
    }
}
