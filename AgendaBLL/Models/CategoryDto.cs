using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaBLL.Models
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

        private StateTypes categoryType;
        public StateTypes CategoryType
        {
            get { return categoryType; }
            set
            {
                categoryType = value;
                NotifyPropertyChanged(nameof(CategoryType));
            }
        }

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


        public CategoryDto()
        {
            Tasks = new ObservableCollection<TaskDto>();
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

    public enum StateTypes
    {
        Checklist,
        MultiChecklist,
        Kanban3,
        Kanban5
    }
}