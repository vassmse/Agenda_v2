using AgendaCON.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace AgendaPL.Converters
{
    public class TaskStateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            //string parameterString = parameter as string;

            //string[] parameters = parameterString.Split(new char[] { '|' });


            //var result = ((IEnumerable)value).Cast<object>().ToList();
            //ArrayList collection = new ArrayList(result);
            //ObservableCollection<TaskDto> retCollection = new ObservableCollection<TaskDto>();

            //foreach (var task in collection)
            //{
            //    if (((TaskDto)(task)).State == int.Parse(parameters[0]) || (parameters.Count() > 1 && ((TaskDto)(task)).State == int.Parse(parameters[1])))
            //        retCollection.Add(((TaskDto)(task)));
            //}
            //return retCollection;

            string parameterString = parameter as string;
            var result = ((IEnumerable)value).Cast<object>().ToList();
            ArrayList collection = new ArrayList(result);
            ObservableCollection<TaskDto> retCollection = new ObservableCollection<TaskDto>();

            foreach (var task in collection)
            {
                if (((TaskDto)(task)).State == int.Parse(parameterString))
                    retCollection.Add(((TaskDto)(task)));
            }
            return retCollection;


        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value;
        }
    }
}
