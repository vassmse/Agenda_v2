using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AgendaCON.Models;
using AgendaDAL.Models;
using AgendaDAL.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AgendaDAL.Controllers
{
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private CategoryRepository Repository { get; }

        public CategoryController(AgendaDbContext context)
        {
            Repository = new CategoryRepository(context);
        }

        // GET: api/<controller>
        // Get all categories
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var items = Repository.GetAllItem();
                if (items != null)
                {
                    return Ok(items);
                }
                return BadRequest();
            }
            catch
            {
                return BadRequest();
            }
        }

        // GET api/<controller>/5
        // Get a category
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var item = Repository.GetItem(id);
                if (item != null)
                {
                    return Ok(item);
                }
                return BadRequest();
            }
            catch
            {
                return BadRequest();
            }
        }

        // POST api/<controller>
        // Add new category
        [HttpPost]
        public IActionResult Post([FromBody]CategoryDto category)
        {
            try
            {
                if (category != null)
                {
                    if (Repository.AddItem(category))
                    {
                        return Ok();
                    }
                    else
                    {
                        return BadRequest();
                    }
                }
                return BadRequest();
            }
            catch
            {
                return BadRequest();
            }
        }

        // PUT api/<controller>/5
        // Update a category
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]CategoryDto category)
        {
            try
            {
                if (category != null)
                {
                    if (Repository.UpdateItem(category))
                    {
                        return Ok();
                    }
                    else
                    {
                        return BadRequest();
                    }
                }
                return BadRequest();
            }
            catch
            {
                return BadRequest();
            }
        }

        // DELETE api/<controller>/5
        // Delete a category
        [HttpDelete("{id}")]
        public IActionResult Delete([FromBody] CategoryDto category)
        {
            try
            {
                if (category != null)
                {
                    if (Repository.DeleteItem(category))
                    {
                        return Ok();
                    }
                    else
                    {
                        return BadRequest();
                    }
                }
                return BadRequest();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
