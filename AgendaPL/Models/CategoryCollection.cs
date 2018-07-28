using AgendaCON.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaPL.Models
{
    public class CategoryCollection : INotifyPropertyChanged
    {
        private ObservableCollection<CategoryDto> categories;
        public ObservableCollection<CategoryDto> Categories
        {
            get { return categories; }
            set
            {
                categories = value;
                NotifyPropertyChanged(nameof(Categories));
            }
        }
        
        public ObservableCollection<TaskDto> AllTasks
        {
            get { return getAllTasks(); }
        }

        public ObservableCollection<TaskDto> DailyTasks
        {
            get { return getDailyTasks(); }
        }

        public CategoryCollection()
        {

        }

        private ObservableCollection<TaskDto> getAllTasks()
        {
            var tasks = new ObservableCollection<TaskDto>();
            foreach (var category in Categories)
            {
                foreach (var task in category.Tasks)
                {
                    tasks.Add(task);
                }
            }
            return tasks;
        }

        private ObservableCollection<TaskDto> getDailyTasks()
        {
           return new ObservableCollection<TaskDto>(AllTasks.Where(t => ((t.DeadlineDate.Year == DateTime.Now.Year && t.DeadlineDate.Day == DateTime.Now.Day) || ( t.ScheduledDate.Year == DateTime.Now.Year && t.ScheduledDate.Day == DateTime.Now.Day))).ToList());

        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
