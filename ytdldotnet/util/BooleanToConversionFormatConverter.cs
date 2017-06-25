using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using ytdldotnet.Util;

namespace ytdldotnet.util
{
    class BooleanToConversionFormatConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (System.Convert.ToString(Enum.GetName(typeof(ConversionFormats), value)).Equals(System.Convert.ToString(parameter)))
            {
                return true;
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (System.Convert.ToBoolean(value))
            {
                return Enum.TryParse(parameter.ToString(), out ConversionFormats format);
            }
            return null;
        }
    }
}
