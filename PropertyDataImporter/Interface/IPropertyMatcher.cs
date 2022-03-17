using PropertyDataImporter.Models;

namespace PropertyDataImporter.Interface
{
    public interface IPropertyMatcher
    {
        bool IsMatch(Property agencyProperty, Property databaseProperty);
    }
}
