using PropertyDataImporter.Interface;
using PropertyDataImporter.Models;
using Serilog;
using System;

namespace PropertyDataImporter.ScheduleTask
{
    public class PropertyMatcher : IPropertyMatcher
    {
        private readonly ILogger _logger;

        public PropertyMatcher(ILogger logger)
        {
            _logger = logger;
        }

        public bool IsMatch(Property agencyProperty, Property databaseProperty)
        {
            _logger.Information("Matching agency property with databaseProperty for {0} agency.", agencyProperty.AgencyCode);
            var matcher = CreateMatcher(agencyProperty);
            if (matcher == null)
            {
                _logger.Error("There is no match for agency code {:@agencyCode}", agencyProperty.AgencyCode);
                throw new Exception(string.Format("There is no match for agency code: {0}", agencyProperty.AgencyCode));
            }

            try
            {
                return matcher.IsMatch(agencyProperty, databaseProperty);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "There is an exception handeled");
                throw;
            }
        }

        public IPropertyMatcher CreateMatcher(Property agencyProperty)
        {
            IPropertyMatcher matcher = null;
            switch (agencyProperty.AgencyCode)
            {
                case "OTBRE":
                    matcher = new AddressMatcher();
                    break;
                case "LRE":
                    matcher = new DistanceMatcher();
                    break;
                case "CRE":
                    matcher = new ReverseNameMatcher();
                    break;
            }
            return matcher;
        }
    }
}