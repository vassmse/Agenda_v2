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

        public ObservableCollection<TaskDto> WeeklyTasks
        {
            get { return getWeeklyTasks(); }
        }

        public ObservableCollection<TaskDto> ExpiredTasks
        {
            get { return getExpiredTasks(); }
        }

        public int LastId { get; set; }

        public CategoryCollection()
        {

        }

        public void AddTask(TaskDto task)
        {
            if (task.IsSubTask)
            {
                Categories.Where(c => c.CategoryId == task.ParentCategoryId).First().Tasks.Where(t => t.TaskId == task.ParentTaskId).First().SubTasks.Add(task);
            }
            else
            {
                Categories.Where(c => c.CategoryId == task.ParentCategoryId).First().Tasks.Add(task);
            }
        }


        public void DeleteTask(TaskDto task)
        {
            if (task.IsSubTask)
            {
                Categories.Where(c => c.CategoryId == task.ParentCategoryId).First().Tasks.Where(t => t.TaskId == task.ParentTaskId).First().SubTasks.Remove(task);
            }
            else
            {
                Categories.Where(c => c.CategoryId == task.ParentCategoryId).First().Tasks.Remove(task);
            }
        }

        public void UpdateTask(TaskDto task)
        {
            if (task.IsSubTask)
            {
                var actualCategory = Categories.Where(c => c.CategoryId == task.ParentCategoryId).First();
                var categoryIdx = Categories.IndexOf(actualCategory);

                var actualTask = actualCategory.Tasks.Where(t => t.TaskId == task.ParentTaskId).First();
                var taskIdx = Categories[categoryIdx].Tasks.IndexOf(actualTask);

                var actualSub = Categories[categoryIdx].Tasks[taskIdx].SubTasks.Where(s => s.TaskId == task.TaskId).First();
                var subIdx = Categories[categoryIdx].Tasks[taskIdx].SubTasks.IndexOf(actualSub);

                Categories[categoryIdx].Tasks[taskIdx].SubTasks[subIdx] = task;
            }
            else
            {
                var actualCategory = Categories.Where(c => c.CategoryId == task.ParentCategoryId).First();
                var categoryIdx = Categories.IndexOf(actualCategory);

                var actualTask = actualCategory.Tasks.Where(t => t.TaskId == task.TaskId).First();
                var taskIdx = Categories[categoryIdx].Tasks.IndexOf(actualTask);

                Categories[categoryIdx].Tasks[taskIdx] = task;
            }
        }

        public void UpdateCategory(CategoryDto category)
        {
            var selectedCategory = Categories.Where(c => c.CategoryId == category.CategoryId).First();
            var categoryIdx = Categories.IndexOf(selectedCategory);
            Categories[categoryIdx].Name = category.Name;
            Categories[categoryIdx].CategoryType = category.CategoryType;
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
            return new ObservableCollection<TaskDto>(AllTasks.Where(t => !t.IsSubTask && (t.State < 4) && ((t.HasDeadlineDate && areDaysSame(t.DeadlineDate, DateTime.Now)) || (t.HasScheduledDate && areDaysSame(t.ScheduledDate, DateTime.Now)))).ToList());

        }

        private ObservableCollection<TaskDto> getWeeklyTasks()
        {
            return new ObservableCollection<TaskDto>(AllTasks.Where(t => (!t.IsSubTask) && (t.State < 4) && ((t.HasDeadlineDate && t.DeadlineDate.DayOfYear < DateTime.Now.DayOfYear+8  && t.DeadlineDate.DayOfYear > DateTime.Now.DayOfYear) || (t.HasScheduledDate && t.ScheduledDate.DayOfYear < DateTime.Now.DayOfYear+8  && t.ScheduledDate.DayOfYear > DateTime.Now.DayOfYear))).ToList());

        }

        private ObservableCollection<TaskDto> getExpiredTasks()
        {
            return new ObservableCollection<TaskDto>(AllTasks.Where(t => (!t.IsSubTask) && (t.State<4) && (t.HasDeadlineDate && t.DeadlineDate.DayOfYear < DateTime.Now.DayOfYear) && (t.HasScheduledDate && t.ScheduledDate.DayOfYear < DateTime.Now.DayOfYear)).ToList());
        }

        private bool areDaysSame(DateTime date1, DateTime date2)
        {
            return date1.Year == date2.Year && date1.Month == date2.Month && date1.Day == date2.Day;
        }


        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }




    }
}
