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
    public class CategoryController : ControllerBase
    {
        private CategoryService Service { get; }

        public CategoryController(AgendaDbContext context)
        {
            Service = new CategoryService(context);
        }

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<CategoryDto> Get()
        {
            return Service.GetAllItem();
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public CategoryDto Get(int id)
        {
            return Service.GetItem(id);            
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]CategoryDto item)
        {
            Service.AddItem(item);
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]CategoryDto category)
        {
            Service.UpdateItem(category);
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete([FromBody] CategoryDto category)
        {
            Service.DeleteItem(category);
        }
    }
}
