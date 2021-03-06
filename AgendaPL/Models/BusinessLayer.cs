﻿using AgendaCON.Models;
using AgendaPL.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AgendaPL.Models
{
    public class BusinessLayer
    {
        private AgendaRestClient RestClient { get; set; }
        private MainViewModel ViewModel { get; set; }
        private UserDto UserLoggedIn { get; set; }

        public BusinessLayer(MainViewModel viewModel)
        {
            RestClient = new AgendaRestClient();
            ViewModel = viewModel;
        }

        // Switching logged in users
        public void SwitchUser(UserDto user)
        {
            UserLoggedIn = user;
        }

        // Get all categories from db and fill CategoryCollection
        public ObservableCollection<CategoryDto> GetAllCategories()
        {
            try
            {
                var categories = RestClient.GetAllCategories().Where(c=>c.ParentUserId==UserLoggedIn.UserId).ToList();
                var tasks = GetAllTasks();
                ViewModel.CategoryCollection.LastTaskId = tasks.Count;
                ViewModel.CategoryCollection.LastCategoryId = categories.Count;

                // Tasks
                foreach (var category in categories)
                {
                    foreach (var task in tasks)
                    {
                        if (category.CategoryId == task.ParentCategoryId)
                        {
                            if (!task.IsSubTask)
                                category.Tasks.Add(task);
                        }
                    }
                }

                // SubTasks
                foreach (var category in categories)
                {
                    foreach (var task in tasks)
                    {
                        if (category.CategoryId == task.ParentCategoryId && task.IsSubTask)
                        {
                            category.Tasks.FirstOrDefault(t => t.TaskId == task.ParentTaskId).SubTasks.Add(task);
                        }
                    }
                }
                var a = categories;

                return new ObservableCollection<CategoryDto>(categories);
            }
            catch (Exception)
            {
                return new ObservableCollection<CategoryDto>();
            }
        }

        // Add new category
        public void AddCategory(CategoryDto category)
        {
            int id = ++ViewModel.CategoryCollection.LastCategoryId;
            var newCategory = new CategoryDto {CategoryId=id, Name = ViewModel.NewCategory.Name, CategoryType = ViewModel.NewCategory.CategoryType, ParentUserId=UserLoggedIn.UserId };
            ViewModel.CategoryCollection.Categories.Add(newCategory);
            RestClient.AddCategory(newCategory);
            ViewModel.NavigationViewItems.AddMenuItem(newCategory);
            ViewModel.NewCategory.Name = String.Empty;
        }

        // Update category
        public void UpdateCategory(CategoryDto category)
        {
            RestClient.UpdateCategory(category);
            ViewModel.CategoryCollection.UpdateCategory(category);
            ViewModel.NavigationViewItems.RenameMenuItem(category.CategoryId, category.Name);
            ViewModel.NavigationViewItems.ChangeMenuItemTag(category.CategoryId, category.CategoryType.ToString());
        }

        // Delete category
        public void DeleteCategory(CategoryDto category)
        {
            ViewModel.CategoryCollection.Categories.Remove(category);
            RestClient.DeleteCategory(category);
            ViewModel.NavigationViewItems.DeleteMenuItem(category.CategoryId);
        }

        // Add new task
        public void AddTask(TaskDto task)
        {
            RestClient.AddTask(task);
            ViewModel.CategoryCollection.AddTask(task);
        }

        // Get all tasks
        public ObservableCollection<TaskDto> GetAllTasks()
        {
            try
            {
                var tasks = RestClient.GetAllTasks();
                return new ObservableCollection<TaskDto>(tasks);
            }
            catch (Exception)
            {
                return new ObservableCollection<TaskDto>();
            }
        }

        // Update a task
        public void UpdateTask(TaskDto task)
        {
            RestClient.UpdateTask(task);
            ViewModel.CategoryCollection.UpdateTask(task);
        }

        // Update a task without CategoryCollection change
        public void UpdateTaskState(TaskDto task)
        {
            RestClient.UpdateTask(task);
        }

        // Delete a task
        public void DeleteTask(TaskDto task)
        {
            RestClient.DeleteTask(task);
            ViewModel.IsPanelActive = false;
            ViewModel.CategoryCollection.DeleteTask(task);
        }

        // Authenticate the user, hash the password
        public UserDto AuthenticateUser(UserDto user)
        {
            var md5 = new MD5CryptoServiceProvider();
            var hash = md5.ComputeHash(Encoding.ASCII.GetBytes(user.PasswordHash));
            user.PasswordHash = Encoding.Default.GetString(md5.Hash);
            UserDto isOk = RestClient.AuthenticateUser(user);
            user.PasswordHash = String.Empty;
            return isOk;

        }

        // Register new user
        public bool RegisterUser(UserDto user)
        {
            var md5 = new MD5CryptoServiceProvider();
            var hash = md5.ComputeHash(Encoding.ASCII.GetBytes(user.PasswordHash));
            user.PasswordHash = Encoding.Default.GetString(md5.Hash);
            bool isOK = RestClient.AddUser(user);            
            user.PasswordHash = String.Empty;            
            return isOK;
            
        }

        // Send mail to registrated user
        private void SendMail(string mailTo)
        {
            try
            {
                MailMessage mail = new MailMessage("noreply@agenda.com", mailTo, "Register to Agenda", "You have succesfully registered to the Agenda To-Do application.");
                SmtpClient client = new SmtpClient
                {
                    Port = 25,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Host = "smtp.gmail.com"                    
                };
                client.Send(mail);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
