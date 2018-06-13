using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgendaDAL.Dtos;
using AgendaDAL.Models;
using AgendaDAL.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AgendaDAL.Controllers
{
    [Route("api/[controller]")]
    public class TaskController : ControllerBase
    {
        private TaskService Service { get; }

        public TaskController(AgendaDbContext context)
        {
            Service = new TaskService(context);
        }

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<TaskDto> Get()
        {
            return Service.GetAllItem();
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public TaskDto Get(int id)
        {
            return Service.GetItem(id);
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]TaskDto item)
        {
            Service.AddItem(item);
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]TaskDto category)
        {
            Service.UpdateItem(category);
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete([FromBody] TaskDto category)
        {
            Service.DeleteItem(category);
        }
    }
}