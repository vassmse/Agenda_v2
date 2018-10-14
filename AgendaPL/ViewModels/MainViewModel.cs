﻿using AgendaCON.Models;
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

        public void Register()
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
            }
            catch
            {
                ErrorMessage = "Something went wrong.";
            }
        }

        public void Logout()
        {
            UserLoggedIn = new UserDto();
        }

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

        public void UpdateCategoryAction(CategoryDto category)
        {
            BusinessLayer.UpdateCategory(category);
        }

        public void ChangeCategoryTypeAction(int categoryType)
        {
            CategoryTypes type = (CategoryTypes)categoryType;
            SelectedCategory.CategoryType = type;
            BusinessLayer.UpdateCategory(SelectedCategory);
            RaisePropertyChanged("SelectedCategory");
        }

        public void DeleteCategoryAction(CategoryDto category)
        {
            BusinessLayer.DeleteCategory(category);
        }

        private void AddCategoryAction()
        {
            BusinessLayer.AddCategory(NewCategory);
        }

        #endregion

        #region Task Actions

        public void CheckChangedAction(TaskDto task)
        {
            BusinessLayer.UpdateTask(task);
        }

        public void AddNewTask()
        {
            int id = ++CategoryCollection.LastTaskId;
            var newTask = new TaskDto { TaskId = id, Name = "New Task", Description = "", DeadlineDate = DateTime.Now, ScheduledDate = DateTime.Now, State = 0, ParentCategoryId = SelectedCategory.CategoryId };
            BusinessLayer.AddTask(newTask);
            RaisePropertyChanged(nameof(SelectedCategory));
        }

        public void AddNewSubTask(int parentTaskId)
        {
            int id = ++CategoryCollection.LastTaskId;
            var newTask = new TaskDto { TaskId = id, Name = "New Subtask", Description = "", DeadlineDate = DateTime.Now, ScheduledDate = DateTime.Now, State = 0, ParentCategoryId = SelectedCategory.CategoryId, ParentTaskId = parentTaskId, IsSubTask = true };
            BusinessLayer.AddTask(newTask);
        }

        public void ChangeTaskState(int taskId, int newState)
        {
            var task = CategoryCollection.AllTasks.Where(t => t.TaskId == taskId).First();
            task.State = newState;
            BusinessLayer.UpdateTask(task);
            RaisePropertyChanged(nameof(SelectedCategory));
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

        private void SaveTaskAction()
        {
            BusinessLayer.UpdateTask(SelectedTask);
            RaisePropertyChanged(nameof(SelectedCategory));
        }

        private void DeleteTaskAction()
        {
            BusinessLayer.DeleteTask(SelectedTask);
            RaisePropertyChanged(nameof(SelectedCategory));
        }

        #endregion

    }
}
