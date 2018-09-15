using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace AgendaPL.Converters
{
    public class IntToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (int.Parse(value.ToString()) == 0 || int.Parse(value.ToString()) == 3 || int.Parse(value.ToString()) == 4 || int.Parse(value.ToString()) == 2)
                return false;
            else return true;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if ((bool)value)
                return 1;
            else return 0;
        }
    }
}
