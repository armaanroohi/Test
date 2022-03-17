using PropertyDataImporter.Interface;
using PropertyDataImporter.Models;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace PropertyDataImporter.ScheduleTask
{
    public class AddressMatcher : IPropertyMatcher
    {
        public bool IsMatch(Property agencyProperty, Property databaseProperty)
        {
            var agPropertyName = TextCleanUp(agencyProperty.Name);
            var agPropertyAddress = TextCleanUp(agencyProperty.Address);
            var dbPropertyName = TextCleanUp(databaseProperty.Name);
            var dbPropertyAddress = TextCleanUp(databaseProperty.Address);

            if (agPropertyName.SequenceEqual(dbPropertyName))
                if (agPropertyAddress.SequenceEqual(dbPropertyAddress))
                    return true;
            return false;
        }

        public string[] TextCleanUp(string txt)
        {
            txt = Regex.Replace(txt, @"[^\w\d]", ",").ToLower();
            return txt.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}