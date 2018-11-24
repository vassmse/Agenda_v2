using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgendaCON.Models;
using AgendaDAL.Models;
using AgendaDAL.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AgendaDAL.Controllers
{
    [Route("api/[controller]")]
    public class TaskController : ControllerBase
    {
        private TaskRepository Repository { get; }

        public TaskController(AgendaDbContext context)
        {
            Repository = new TaskRepository(context);
        }

        // GET: api/<controller>
        [HttpGet]
        public IActionResult Get()
        {
            var items = Repository.GetAllItem();
            if (items != null)
            {
                return Ok(items);
            }
            return BadRequest();
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var item = Repository.GetItem(id);
            if (item != null)
            {
                return Ok(item);
            }
            return BadRequest();
        }

        // POST api/<controller>
        [HttpPost]
        public IActionResult Post([FromBody]TaskDto task)
        {
            if (task != null)
            {
                if (Repository.AddItem(task))
                {
                    return Ok();
                }
                return BadRequest();
            }
            return BadRequest();
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]TaskDto task)
        {
            if (task != null)
            {
                if (Repository.UpdateItem(task))
                {
                    return Ok();
                }
                return BadRequest();
            }
            return BadRequest();
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public IActionResult Delete([FromBody] TaskDto task)
        {
            if (task != null)
            {
                if (Repository.DeleteItem(task))
                {
                    return Ok();
                }
                return BadRequest();
            }
            return BadRequest();
        }
    }
}
