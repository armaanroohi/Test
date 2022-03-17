using PropertyDataImporter.Interface;
using PropertyDataImporter.Models;
using System;

namespace PropertyDataImporter.ScheduleTask
{
    public class DistanceMatcher : IPropertyMatcher
    {
        public bool IsMatch(Property agProperty, Property dbProperty)
        {
            var deltaLat = agProperty.Latitude - dbProperty.Latitude;
            var deltaLong = agProperty.Longitude - dbProperty.Longitude;
            var distance = Math.Sqrt(Math.Pow((double)deltaLong, 2) + Math.Pow((double)deltaLat, 2));
            return distance * 111 <= 0.2;
        }
    }
}