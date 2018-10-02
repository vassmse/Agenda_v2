using AgendaCON.Models;
using AgendaPL.Models;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Resources;
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Controls;
using GalaSoft.MvvmLight.Command;

namespace AgendaPL.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        #region RelayCommand properties

        public RelayCommand AddCategoryCommand { get; private set; }

        public RelayCommand<int> TaskSelectedCommand { get; private set; }

        public RelayCommand SaveTaskCommand { get; private set; }

        public RelayCommand DeleteTaskCommand { get; private set; }


        #endregion

        public CategoryCollection CategoryCollection { get; set; }

        private BusinessLayer businessLayer { get; set; }

        public NavigationViewMenuItems NavigationViewItems { get; set; }

        public CategoryDto NewCategory { get; set; }

        private CategoryDto selectedCategory;
        public CategoryDto SelectedCategory
        {
            get { return selectedCategory; }
            set
            {
                selectedCategory = value;
                RaisePropertyChanged(nameof(SelectedCategory));
            }
        }

        private CategoryTypes categoryType;

        public CategoryTypes CategoryType
        {
            get { return categoryType; }
            set
            {
                categoryType = value;
                RaisePropertyChanged(nameof(CategoryType));
            }
        }


        private TaskDto selectedTask;

        public TaskDto SelectedTask
        {
            get { return selectedTask; }
            set
            {
                selectedTask = value;
                RaisePropertyChanged(nameof(SelectedTask));
            }
        }


        private bool isPanelActive;

        public bool IsPanelActive
        {
            get { return isPanelActive; }
            set
            {
                isPanelActive = value;
                RaisePropertyChanged(nameof(IsPanelActive));
            }
        }




        public MainViewModel()
        {
            businessLayer = new BusinessLayer(this);
            NewCategory = new CategoryDto();
            CategoryCollection = new CategoryCollection();
            CategoryCollection.Categories = businessLayer.GetAllCategories();
            NavigationViewItems = new NavigationViewMenuItems(CategoryCollection.Categories);
            SelectedCategory = new CategoryDto();
            SelectedTask = new TaskDto();


            #region RelayCommands

            SaveTaskCommand = new RelayCommand(SaveTaskAction);
            AddCategoryCommand = new RelayCommand(AddCategoryAction);
            DeleteTaskCommand = new RelayCommand(DeleteTaskAction);


            #endregion

        }

        #region Commands

        private void AddCategoryAction()
        {
            businessLayer.AddCategory(NewCategory);
        }

        public void SelectedTaskAction(TaskDto task)
        {
            if (SelectedTask.TaskId == task.TaskId)
                IsPanelActive = !IsPanelActive;
            else if (!task.IsSubTask)
            {
                SelectedTask = SelectedCategory.Tasks.Where(t => t.TaskId == task.TaskId).FirstOrDefault();
                IsPanelActive = true;
            }
            else
            {
                SelectedTask = SelectedCategory.Tasks.Where(t => t.TaskId == task.ParentTaskId).FirstOrDefault().SubTasks.Where(t => t.TaskId == task.TaskId).FirstOrDefault();
                IsPanelActive = true;
            }

        }

        public void SaveTaskAction()
        {
            businessLayer.UpdateTask(SelectedTask);
            RaisePropertyChanged(nameof(SelectedCategory));
        }

        public void CheckChangedAction(TaskDto task)
        {
            businessLayer.UpdateTask(task);
        }

        public void DeleteCategoryAction(CategoryDto category)
        {
            businessLayer.DeleteCategory(category);
        }

        public void AddNewTask()
        {
            int id = ++CategoryCollection.LastId;
            var newTask = new TaskDto { TaskId = id, Name = "New Task", Description = "", DeadlineDate = DateTime.Now, ScheduledDate = DateTime.Now, State = 0, ParentCategoryId = SelectedCategory.CategoryId };
            businessLayer.AddTask(newTask);
            RaisePropertyChanged(nameof(SelectedCategory));
        }

        public void AddNewSubTask(int parentTaskId)
        {
            int id = ++CategoryCollection.LastId;
            var newTask = new TaskDto { TaskId = id, Name = "New Subtask", Description = "", DeadlineDate = DateTime.Now, ScheduledDate = DateTime.Now, State = 0, ParentCategoryId = SelectedCategory.CategoryId, ParentTaskId = parentTaskId, IsSubTask = true };
            businessLayer.AddTask(newTask);
        }

        public void DeleteTaskAction()
        {
            businessLayer.DeleteTask(SelectedTask);
            RaisePropertyChanged(nameof(SelectedCategory));
        }

        public void ChangeTaskState(int taskId, int newState)
        {
            var task = CategoryCollection.AllTasks.Where(t => t.TaskId == taskId).First();
            task.State = newState;
            businessLayer.UpdateTask(task);
            RaisePropertyChanged(nameof(SelectedCategory));
        }

        public void RenameCategoryAction(CategoryDto category)
        {
            if (!category.RenamingInProgress)
            {
                category.RenamingInProgress = true;
            }
            else
            {
                category.RenamingInProgress = false;
                businessLayer.UpdateCategory(category);
            }
        }


        #endregion

    }
}
