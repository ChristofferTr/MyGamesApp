using System;
using System.Globalization;
using System.Windows.Data;

namespace MyGamesApp
{
    public class IntToYesNoConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int intValue)
            {
                return intValue == 1 ? "Yes" : "No";
            }
            return "No"; // Default value if not an integer
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}