using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaCON.Models
{
    public class TaskDto : INotifyPropertyChanged
    {
        private int taskId;
        public int TaskId
        {
            get { return taskId; }
            set
            {
                taskId = value;
                NotifyPropertyChanged(nameof(TaskId));
            }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                NotifyPropertyChanged(nameof(Name));
            }
        }

        private string description;
        public string Description
        {
            get { return description; }
            set
            {
                description = value;
                NotifyPropertyChanged(nameof(Description));
            }
        }

        private int state;
        public int State
        {
            get { return state; }
            set
            {
                state = value;
                NotifyPropertyChanged(nameof(State));
            }
        }

        private DateTime deadlineDate;
        public DateTime DeadlineDate
        {
            get { return deadlineDate; }
            set
            {
                deadlineDate = value;
                NotifyPropertyChanged(nameof(DeadlineDate));
            }
        }

        private DateTime createdDate;
        public DateTime CreatedDate
        {
            get { return createdDate; }
            set
            {
                createdDate = value;
                NotifyPropertyChanged(nameof(CreatedDate));
            }
        }

        private DateTime scheduledDate;
        public DateTime ScheduledDate
        {
            get { return scheduledDate; }
            set
            {
                scheduledDate = value;
                NotifyPropertyChanged(nameof(ScheduledDate));
            }
        }

        private ObservableCollection<TaskDto> subTasks;

        public ObservableCollection<TaskDto> SubTasks
        {
            get { return subTasks; }
            set
            {
                subTasks = value;
                NotifyPropertyChanged(nameof(SubTasks));
            }
        }

        private int parentTaskId;
        public int ParentTaskId
        {
            get { return parentTaskId; }
            set
            {
                parentTaskId = value;
                NotifyPropertyChanged(nameof(ParentTaskId));
            }
        }

        private int parentCategoryId;
        public int ParentCategoryId
        {
            get { return parentCategoryId; }
            set
            {
                parentCategoryId = value;
                NotifyPropertyChanged(nameof(ParentCategoryId));
            }
        }        

        public TaskDto()
        {
            SubTasks = new ObservableCollection<TaskDto>();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
