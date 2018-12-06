using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaCON.Models
{
    // Data Transfer Object for Tasks
    public class TaskDto : INotifyPropertyChanged
    {
        //Id of the task
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

        //Name of the task
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

        //Description of the task
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

        //State can be: 0: Backlog, 1: ToDo, 2: InProgress, 3: Testing, 4: Ready
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

        //Deadline date for the task
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

        //Weather the task has deadline date
        private bool hasDeadlineDate;
        public bool HasDeadlineDate
        {
            get { return hasDeadlineDate; }
            set
            {
                hasDeadlineDate = value;
                NotifyPropertyChanged(nameof(HasDeadlineDate));
            }
        }

        //The creation date for the task
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

        //Weather the task has schedule date
        private bool hasScheduledDate;
        public bool HasScheduledDate
        {
            get { return hasScheduledDate; }
            set
            {
                hasScheduledDate = value;
                NotifyPropertyChanged(nameof(HasScheduledDate));
            }
        }

        //Schedule date for the task
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

        //Subtasks in multichecklist
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

        //Is the task subtask
        private bool isSubTask;
        public bool IsSubTask
        {
            get { return isSubTask; }
            set
            {
                isSubTask = value;
                NotifyPropertyChanged(nameof(IsSubTask));
            }
        }

        //If the task is subtask -> id of the parent task
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

        //Owner category id
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
