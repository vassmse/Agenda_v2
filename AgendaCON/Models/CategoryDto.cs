using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaCON.Models
{
    // Data Transfer Object for Category
    public class CategoryDto : INotifyPropertyChanged
    {
        // ID for the category
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

        //Name of the category
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

        //Category type can be Checklist, MultiChecklist, Kanban3, Kanban5
        public CategoryTypes CategoryType { get; set; }

        //Category's tasks
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

        //Category owner user
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

        //Category types in array
        public Array CategoryTypeValues { get; set; }

        //User is renaming the category
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

    //Category types can be four different
    public enum CategoryTypes
    {
        Checklist,
        MultiChecklist,
        Kanban3,
        Kanban5
    }
}