using AgendaCON.Models;
using AgendaPL.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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

        public void SwitchUser(UserDto user)
        {
            UserLoggedIn = user;
            RestClient.SwitchUser(user);
        }

        public ObservableCollection<CategoryDto> GetAllCategories()
        {
            try
            {
                //TODO
                var categories = RestClient.GetAllCategories().Where(c=>c.ParentUserId==UserLoggedIn.UserId).ToList();
                var tasks = GetAllTasks();
                ViewModel.CategoryCollection.LastId = tasks.Count + 1;

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

                //SubTasks
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

        public void AddCategory(CategoryDto category)
        {
            var newCategory = new CategoryDto { Name = ViewModel.NewCategory.Name, CategoryType = ViewModel.NewCategory.CategoryType, ParentUserId=UserLoggedIn.UserId };
            ViewModel.CategoryCollection.Categories.Add(newCategory);
            RestClient.AddCategory(category);
            ViewModel.NavigationViewItems.AddMenuItem(newCategory);
            ViewModel.NewCategory.Name = String.Empty;
        }

        public void UpdateCategory(CategoryDto category)
        {
            RestClient.UpdateCategory(category);
            ViewModel.CategoryCollection.UpdateCategory(category);
            ViewModel.NavigationViewItems.RenameMenuItem(category.CategoryId, category.Name);
            ViewModel.NavigationViewItems.ChangeMenuItemTag(category.CategoryId, category.CategoryType.ToString());
        }

        public void DeleteCategory(CategoryDto category)
        {
            ViewModel.CategoryCollection.Categories.Remove(category);
            RestClient.DeleteCategory(category);
            ViewModel.NavigationViewItems.DeleteMenuItem(category.CategoryId);
        }

        public void AddTask(TaskDto task)
        {
            RestClient.AddTask(task);
            ViewModel.CategoryCollection.AddTask(task);
        }

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

        public void UpdateTask(TaskDto task)
        {
            RestClient.UpdateTask(task);
            ViewModel.CategoryCollection.UpdateTask(task);
        }

        public void DeleteTask(TaskDto task)
        {
            RestClient.DeleteTask(task);
            ViewModel.IsPanelActive = false;
            ViewModel.CategoryCollection.DeleteTask(task);
        }

        public UserDto AuthenticateUser(UserDto user)
        {
            var md5 = new MD5CryptoServiceProvider();
            var hash = md5.ComputeHash(Encoding.ASCII.GetBytes(user.PasswordHash));
            user.PasswordHash = Encoding.Default.GetString(md5.Hash);
            UserDto isOk = RestClient.AuthenticateUser(user);
            user.PasswordHash = String.Empty;
            return isOk;

        }

        public bool RegisterUser(UserDto user)
        {
            var md5 = new MD5CryptoServiceProvider();
            var hash = md5.ComputeHash(Encoding.ASCII.GetBytes(user.PasswordHash));
            user.PasswordHash = Encoding.Default.GetString(md5.Hash);
            bool isOK = RestClient.AddUser(user);            
            user.PasswordHash = String.Empty;
            return isOK;
            //MailMessage mail = new MailMessage("noreply@agenda.com", user.Email, "Register to Agenda", "You have succesfully registered to the Agenda To-Do application.");
            //SmtpClient client = new SmtpClient();
            //client.Port = 25;
            //client.DeliveryMethod = SmtpDeliveryMethod.Network;
            //client.UseDefaultCredentials = false;
            //client.Host = "smtp.gmail.com";
            //client.Send(mail);
        }
    }
}
