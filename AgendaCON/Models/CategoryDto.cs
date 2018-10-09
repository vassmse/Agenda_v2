using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaCON.Models
{
    public class CategoryDto : INotifyPropertyChanged
    {
        private int categoryId;
        public int CategoryId
        {
            get { return categoryId; }
            set
            {
                categoryId = value;
                NotifyPropertyChanged(nameof(CategoryId));
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

        public CategoryTypes CategoryType { get; set; }

        private ObservableCollection<TaskDto> tasks;
        public ObservableCollection<TaskDto> Tasks
        {
            get { return tasks; }
            set
            {
                tasks = value;
                NotifyPropertyChanged(nameof(Tasks));
            }
        }

        private int parentUserId;
        public int ParentUserId
        {
            get { return parentUserId; }
            set
            {
                parentUserId = value;
                NotifyPropertyChanged(nameof(ParentUserId));
            }
        }

        public Array CategoryTypeValues { get; set; }

        private bool renamingInProgress;
        public bool RenamingInProgress
        {
            get { return renamingInProgress; }
            set
            {
                renamingInProgress = value;
                NotifyPropertyChanged(nameof(RenamingInProgress));
            }
        }

        public CategoryDto()
        {
            Tasks = new ObservableCollection<TaskDto>();
            CategoryTypeValues = Enum.GetValues(typeof(CategoryTypes));
        }

        public override string ToString()
        {
            return Name;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public enum CategoryTypes
    {
        Checklist,
        MultiChecklist,
        Kanban3,
        Kanban5
    }
}