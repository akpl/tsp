using System;
using System.Globalization;
using System.Windows.Data;
using TSP.Solver;

namespace TSP
{
    public class StringToCoordinatesConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var coords = value as Coordinates?;
            return coords?.ToDecimalDegreesString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string coordsText = value.ToString();
            int commaIndex = coordsText.IndexOf(",", StringComparison.Ordinal);
            if (commaIndex < 0)
            {
                return null;
            }
            double latitude, longitude;
            if (!double.TryParse(coordsText.Substring(0, commaIndex), out latitude) ||
                !double.TryParse(coordsText.Substring(commaIndex+1), out longitude))
            {
                return null;
            }
            return new Coordinates(latitude, longitude);
            
        }
    }
}
