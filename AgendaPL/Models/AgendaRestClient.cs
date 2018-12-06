using AgendaCON.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaPL.Models
{
    // Class for REST communication
    public class AgendaRestClient
    {
        private RestClient Client { get; set; }

        public AgendaRestClient()
        {
            // Set port
            Client = new RestClient("http://localhost:60225/api/");
        }

        #region Category CRUD

        // Request for all categories, syncron
        public List<CategoryDto> GetAllCategories()
        {
            try
            {
                var request = new RestRequest("category", Method.GET);
                var response = Client.Execute<List<CategoryDto>>(request);
                return JsonConvert.DeserializeObject<List<CategoryDto>>(response.Content);
            }
            catch
            {
                return null;
            }
        }

        // Request for a category, asyncron
        public async void AddCategory(CategoryDto category)
        {
            try
            {
                var request = new RestRequest("category", Method.POST);
                request.AddJsonBody(category);
                await Client.ExecuteTaskAsync(request);
            }
            catch { }
        }

        // Request for deleting a category, asyncron
        public async void DeleteCategory(CategoryDto category)
        {
            try
            {
                var request = new RestRequest("category/{" + category.CategoryId + "}", Method.DELETE);
                request.AddJsonBody(category);
                await Client.ExecuteTaskAsync(request);
            }
            catch { }
        }

        // Request for updating a category, asyncron
        public async void UpdateCategory(CategoryDto category)
        {
            try
            {
                var request = new RestRequest("category/{" + category.CategoryId + "}", Method.PUT);
                request.AddJsonBody(category);
                await Client.ExecuteTaskAsync(request);
            }
            catch { }
        }

        #endregion

        #region Task CRUD

        // Request for all the tasks, syncron
        public List<TaskDto> GetAllTasks()
        {
            try
            {
                var request = new RestRequest("task", Method.GET);
                var response = Client.Execute<List<CategoryDto>>(request);
                return JsonConvert.DeserializeObject<List<TaskDto>>(response.Content);
            }
            catch
            {
                return null;
            }
        }

        // Request for a task, asyncron
        public async void AddTask(TaskDto task)
        {
            try
            {
                var request = new RestRequest("task", Method.POST);
                request.AddJsonBody(task);
                await Client.ExecuteTaskAsync<List<CategoryDto>>(request);
            }
            catch { }
        }

        // Request for updating a task, asyncron
        public async void UpdateTask(TaskDto task)
        {
            try
            {
                var request = new RestRequest("task/" + task.TaskId, Method.PUT);
                request.AddJsonBody(task);
                await Client.ExecuteTaskAsync<List<CategoryDto>>(request);
            }
            catch { }
        }

        // Request for deleting a task, asyncron
        public async void DeleteTask(TaskDto task)
        {
            try
            {
                var request = new RestRequest("task/" + task.TaskId, Method.DELETE);
                request.AddJsonBody(task);
                var response = await Client.ExecuteTaskAsync<List<CategoryDto>>(request);
                Console.WriteLine(response.Content);
            }
            catch  { }
        }

        #endregion

        #region User CRUD

        // Request for authenticate a user, syncron
        public UserDto AuthenticateUser(UserDto user)
        {
            try
            {
                var request = new RestRequest("user/authenticate", Method.POST);
                request.AddJsonBody(user);
                var response = Client.Execute<UserDto>(request);
                return JsonConvert.DeserializeObject<UserDto>(response.Content);
            }
            catch
            {
                return null;
            }
        }

        // Request for adding a user, syncron
        public bool AddUser(UserDto user)
        {
            try
            {
                var request = new RestRequest("user", Method.POST);
                request.AddJsonBody(user);
                var response = Client.Execute<List<CategoryDto>>(request);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        #endregion
    }
}
