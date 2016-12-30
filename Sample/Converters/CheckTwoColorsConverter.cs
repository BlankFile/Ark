using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Sample.Converters
{
    internal class CheckTwoColorsConverter : IMultiValueConverter
    {
        public object Convert(object[] value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value[0] == null || value[1] == null)
                return false;

            var color1 = value[0] as SolidColorBrush;
            var color2 = value[1] as SolidColorBrush;

            if (color1 == null || color2 == null)
                return false;

            return (color1.Color == color2.Color);
        }

        public object[] ConvertBack(object value, Type[] targetType, object parameter, CultureInfo culture)
        {
            throw new Exception();
        }

    }
}
