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
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace AgendaPL.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        #region RelayCommand properties

        public RelayCommand AddCategoryCommand { get; private set; }

        public RelayCommand SaveTaskCommand { get; private set; }

        public RelayCommand DeleteTaskCommand { get; private set; }

        #endregion

        #region Properties

        public CategoryCollection CategoryCollection { get; set; }

        private BusinessLayer BusinessLayer { get; set; }

        public NavigationViewMenuItems NavigationViewItems { get; set; }

        public CategoryDto NewCategory { get; set; }

        #endregion

        #region Full Properties with RaisePropertyChanged

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

        private UserDto userLoggedIn;
        public UserDto UserLoggedIn
        {
            get { return userLoggedIn; }
            set
            {
                userLoggedIn = value;
                RaisePropertyChanged(nameof(UserLoggedIn));

            }
        }

        private string errorMessage;
        public string ErrorMessage
        {
            get { return errorMessage; }
            set
            {
                errorMessage = value;
                RaisePropertyChanged(nameof(ErrorMessage));
            }
        }

        private string okMessage;
        public string OkMessage
        {
            get { return okMessage; }
            set
            {
                okMessage = value;
                RaisePropertyChanged(nameof(OkMessage));
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

        #endregion

        public MainViewModel()
        {
            #region Init objects

            BusinessLayer = new BusinessLayer(this);
            NewCategory = new CategoryDto();
            CategoryCollection = new CategoryCollection();
            SelectedCategory = new CategoryDto();
            SelectedTask = new TaskDto();
            UserLoggedIn = new UserDto();

            #endregion

            #region RelayCommands

            SaveTaskCommand = new RelayCommand(SaveTaskAction);
            AddCategoryCommand = new RelayCommand(AddCategoryAction);
            DeleteTaskCommand = new RelayCommand(DeleteTaskAction);

            #endregion
        }

        #region User actions

        // User clicked on login button -> authenticate the user
        public bool LoginButtonAction()
        {
            try
            {
                if (IsValidEmail(UserLoggedIn.Email) && UserLoggedIn.PasswordHash.Length > 4)
                {
                    UserLoggedIn = BusinessLayer.AuthenticateUser(UserLoggedIn);
                    if (UserLoggedIn != null)
                    {
                        UserLoggedIn.IsLoggedIn = true;
                        BusinessLayer.SwitchUser(UserLoggedIn);
                        CategoryCollection.Categories = BusinessLayer.GetAllCategories();
                        NavigationViewItems = new NavigationViewMenuItems(CategoryCollection.Categories);
                        NavigationViewItems.SetUserEmail(UserLoggedIn.Email);
                        return true;
                    }
                    else
                    {
                        ErrorMessage = "Username or password is incorrect";
                        return false;
                    }
                }
                else
                {
                    ErrorMessage = "Username or password is incorrect";
                    return false;
                }
            }
            catch
            {
                ErrorMessage = "Username or password is incorrect";
                return false;
            }
        }

        // Register a new user
        public bool Register()
        {
            try
            {
                OkMessage = String.Empty;
                ErrorMessage = String.Empty;

                if (IsValidEmail(UserLoggedIn.Email))
                {
                    if (UserLoggedIn.PasswordHash.Length > 4)
                    {
                        if (BusinessLayer.RegisterUser(UserLoggedIn))
                        {
                            OkMessage = "Your registration is complete. Now you can log in.";
                            return true;
                        }
                        else
                        {
                            ErrorMessage = "Email is already taken.";
                        }
                    }
                    else
                    {
                        ErrorMessage = "Password must be longer than 4 characters.";
                    }
                }
                else
                {
                    ErrorMessage = "Please give a valid email address.";
                }
                return false;
            }
            catch
            {
                ErrorMessage = "Something went wrong.";
                return false;
            }
        }

        // User logged out
        public void Logout()
        {
            UserLoggedIn = new UserDto();
        }

        // Is the given email is valid
        private bool IsValidEmail(string email)
        {
            try
            {
                MailAddress mail = new MailAddress(email);
                return true;
            }
            catch
            {
                return false;
            }
        }
        
        #endregion

        #region Category actions

        // Rename the selected category
        public void RenameCategoryAction(CategoryDto category)
        {
            if (!category.RenamingInProgress)
            {
                category.RenamingInProgress = true;
            }
            else
            {
                category.RenamingInProgress = false;
                BusinessLayer.UpdateCategory(category);
            }
        }

        // Update the cateogory
        public void UpdateCategoryAction(CategoryDto category)
        {
            BusinessLayer.UpdateCategory(category);
        }

        // Change category type
        public void ChangeCategoryTypeAction(int categoryType)
        {
            CategoryTypes type = (CategoryTypes)categoryType;
            SelectedCategory.CategoryType = type;
            BusinessLayer.UpdateCategory(SelectedCategory);
            RaisePropertyChanged("SelectedCategory");
        }

        // Delete the category
        public void DeleteCategoryAction(CategoryDto category)
        {
            BusinessLayer.DeleteCategory(category);
        }

        // Add new category
        private void AddCategoryAction()
        {
            BusinessLayer.AddCategory(NewCategory);
        }

        #endregion

        #region Task Actions

        // User moved or ticket the task
        public void CheckChangedAction(TaskDto task)
        {
            BusinessLayer.UpdateTask(task);
        }

        // Adding new task
        public void AddNewTask()
        {
            int id = ++CategoryCollection.LastTaskId;
            var newTask = new TaskDto { TaskId = id, Name = "New Task", Description = "", DeadlineDate = DateTime.Now, ScheduledDate = DateTime.Now, State = 0, ParentCategoryId = SelectedCategory.CategoryId, CreatedDate=DateTime.Now };
            BusinessLayer.AddTask(newTask);
            RaisePropertyChanged(nameof(SelectedCategory));
        }
        
        // Adding new subtask
        public void AddNewSubTask(int parentTaskId)
        {
            int id = ++CategoryCollection.LastTaskId;
            var newTask = new TaskDto { TaskId = id, Name = "New Subtask", Description = "", DeadlineDate = DateTime.Now, ScheduledDate = DateTime.Now, State = 0, ParentCategoryId = SelectedCategory.CategoryId, ParentTaskId = parentTaskId, IsSubTask = true, CreatedDate = DateTime.Now };
            BusinessLayer.AddTask(newTask);
        }

        // Change tha state of the task
        public void ChangeTaskState(int taskId, int newState)
        {
            var task = CategoryCollection.AllTasks.Where(t => t.TaskId == taskId).First();
            task.State = newState;
            task.State = newState;
            BusinessLayer.UpdateTaskState(task);
            RaisePropertyChanged(nameof(SelectedCategory));
        }

        // Select a task
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

        // Update the selected task
        private void SaveTaskAction()
        {
            BusinessLayer.UpdateTask(SelectedTask);
            RaisePropertyChanged(nameof(SelectedCategory));
        }

        // Delete the selected task
        private void DeleteTaskAction()
        {
            BusinessLayer.DeleteTask(SelectedTask);
            RaisePropertyChanged(nameof(SelectedCategory));
        }

        #endregion

    }
}
