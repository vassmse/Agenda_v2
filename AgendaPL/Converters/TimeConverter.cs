using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace AgendaPL.Converters
{
    // Time converter
    public class TimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return new DateTimeOffset(((DateTime)value).ToUniversalTime());

        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (value != null)
                return ((DateTimeOffset)value).DateTime;
            else
                return DateTime.Now;
        }
    }
}
