using System;
using System.Globalization;
using System.Windows.Data;

namespace Sample.Converters
{
    internal class CheckTwoStringConverter : IMultiValueConverter
    {
        public object Convert(object[] value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value[0] == null || value[1] == null)
                return false;

            var string1 = value[0] as string;
            var string2 = value[1] as string;

            if (string1.IsNullOrEmpty() || string2.IsNullOrEmpty())
                return false;

            return (string1 == string2);
        }

        public object[] ConvertBack(object value, Type[] targetType, object parameter, CultureInfo culture)
        {
            throw new Exception();
        }

    }
}
