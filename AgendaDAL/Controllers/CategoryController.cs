using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AgendaDAL.Dtos;
using AgendaDAL.Models;
using AgendaDAL.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AgendaDAL.Controllers
{
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private CategoryService Service { get; }

        public CategoryController(AgendaDbContext context)
        {
            Service = new CategoryService(context);
        }

        // GET: api/<controller>
        [HttpGet]
        public IActionResult Get()
        {
            var items = Service.GetAllItem();
            if (items != null)
            {
                return Ok(items);
            }
            return NotFound();
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var item = Service.GetItem(id);
            if (item != null)
            {
                return Ok(item);
            }
            return NotFound();
        }

        // POST api/<controller>
        [HttpPost]
        public IActionResult Post([FromBody]CategoryDto item)
        {
            if (item != null)
            {
                Service.AddItem(item);
                return Ok();
            }
            return BadRequest();
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]CategoryDto item)
        {            
            if (item != null)
            {
                Service.UpdateItem(item);
                return Ok();
            }
            return BadRequest();
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public IActionResult Delete([FromBody] CategoryDto item)
        {            
            if (item != null)
            {
                Service.DeleteItem(item);
                return Ok();
            }
            return BadRequest();
        }
    }
}
