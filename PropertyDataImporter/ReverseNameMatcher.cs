using PropertyDataImporter.Interface;
using PropertyDataImporter.Models;
using System;
using System.Linq;

namespace PropertyDataImporter.ScheduleTask
{
    public class ReverseNameMatcher : IPropertyMatcher
    {
        public bool IsMatch(Property agencyProperty, Property databaseProperty)
        {
            var agName = GetWords(agencyProperty.Name);
            var dbName = GetWords(databaseProperty.Name);

            return agName.SequenceEqual(dbName.Reverse());
        }

        protected string[] GetWords(string txt)
        {
            return txt.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}