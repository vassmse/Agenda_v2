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
    public class AgendaRestClient
    {
        private RestClient Client { get; set; }

        public AgendaRestClient()
        {
            Client = new RestClient("http://localhost:60225/api/");
        }

        #region Category CRUD

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
                //TODO: Log
                return null;
            }
        }

        public async void AddCategory(CategoryDto category)
        {
            try
            {
                var request = new RestRequest("category", Method.POST);
                request.AddJsonBody(category);
                await Client.ExecuteTaskAsync(request);
            }
            catch
            {
                //TODO: Log
            }
        }

        public async void DeleteCategory(CategoryDto category)
        {
            try
            {
                var request = new RestRequest("category/{" + category.CategoryId + "}", Method.DELETE);
                request.AddJsonBody(category);
                await Client.ExecuteTaskAsync(request);
            }
            catch
            {
                //TODO: Log
            }
        }

        public async void UpdateCategory(CategoryDto category)
        {
            try
            {
                var request = new RestRequest("category/{" + category.CategoryId + "}", Method.PUT);
                request.AddJsonBody(category);
                await Client.ExecuteTaskAsync(request);
            }
            catch
            {
                //TODO: Log
            }
        }

        #endregion

        #region Task CRUD

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
                //TODO: Log
                return null;
            }
        }

        public async void AddTask(TaskDto task)
        {
            try
            {
                var request = new RestRequest("task", Method.POST);
                request.AddJsonBody(task);
                await Client.ExecuteTaskAsync<List<CategoryDto>>(request);
            }
            catch
            {
                //TODO: Log
            }
        }

        public async void UpdateTask(TaskDto task)
        {
            try
            {
                var request = new RestRequest("task/" + task.TaskId, Method.PUT);
                request.AddJsonBody(task);
                await Client.ExecuteTaskAsync<List<CategoryDto>>(request);
            }
            catch
            {
                //TODO: Log
            }
        }

        public async void DeleteTask(TaskDto task)
        {
            try
            {
                var request = new RestRequest("task/" + task.TaskId, Method.DELETE);
                request.AddJsonBody(task);
                var response = await Client.ExecuteTaskAsync<List<CategoryDto>>(request);
                Console.WriteLine(response.Content);
            }
            catch
            {
                //TODO: Log
            }
        }

        #endregion

        #region User CRUD

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
                //TODO: Log
                return null;
            }
        }

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
                //TODO: Log
                return false;
            }
        }

        #endregion
    }
}
