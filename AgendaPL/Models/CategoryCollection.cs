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
            return new ObservableCollection<TaskDto>(AllTasks.Where(t => ((t.DeadlineDate.Year == DateTime.Now.Year && t.DeadlineDate.Day == DateTime.Now.Day) || (t.ScheduledDate.Year == DateTime.Now.Year && t.ScheduledDate.Day == DateTime.Now.Day))).ToList());

        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }




    }
}
