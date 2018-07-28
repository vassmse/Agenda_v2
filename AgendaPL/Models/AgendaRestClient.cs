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
            var request = new RestRequest("category", Method.GET);
            var response = Client.Execute<List<CategoryDto>>(request);
            return JsonConvert.DeserializeObject<List<CategoryDto>>(response.Content);
        }

        public async void AddCategory(CategoryDto category)
        {
            var request = new RestRequest("category", Method.POST);
            request.AddJsonBody(category);
            await Client.ExecuteTaskAsync<List<CategoryDto>>(request);
        }

        public async void DeleteCategory(CategoryDto category)
        {
            var request = new RestRequest("category/{" + category.CategoryId + "}", Method.DELETE);
            request.AddJsonBody(category);
            await Client.ExecuteTaskAsync(request);
        }

        public async void UpdateCategory(CategoryDto category)
        {
            var request = new RestRequest("category/{" + category.CategoryId + "}", Method.PUT);
            request.AddJsonBody(category);
            await Client.ExecuteTaskAsync(request);
        }

        #endregion

        #region Task CRUD

        public List<TaskDto> GetAllTasks()
        {
            var request = new RestRequest("task", Method.GET);
            var response = Client.Execute<List<CategoryDto>>(request);
            return JsonConvert.DeserializeObject<List<TaskDto>>(response.Content);
        }

        public async void AddTask(TaskDto task)
        {
            var request = new RestRequest("task", Method.POST);
            request.AddJsonBody(task);
            await Client.ExecuteTaskAsync<List<CategoryDto>>(request);
        }

        public async void UpdateTask(TaskDto task)
        {
            var request = new RestRequest("task/" + task.TaskId, Method.PUT);
            request.AddJsonBody(task);
            await Client.ExecuteTaskAsync<List<CategoryDto>>(request);
        }

        public async void DeleteTask(TaskDto task)
        {
            var request = new RestRequest("task/" + task.TaskId, Method.DELETE);
            request.AddJsonBody(task);
            var response = await Client.ExecuteTaskAsync<List<CategoryDto>>(request);
            Console.WriteLine(response.Content);
        }
        #endregion

        #region User CRUD
        #endregion
    }
}
