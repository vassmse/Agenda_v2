using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace AgendaPL.Converters
{
    public class LengthToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            try
            {
                if (Int32.Parse(value.ToString().Length.ToString()) > 0)
                    return Visibility.Visible;
                else return Visibility.Collapsed;
            }
            catch (Exception)
            {
                return Visibility.Collapsed;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            try
            {
                if (value.ToString() == "Visible")
                    return 1;
                else return 0;
            }
            catch (Exception)
            {
                return 0;
            }

        }
    }
}