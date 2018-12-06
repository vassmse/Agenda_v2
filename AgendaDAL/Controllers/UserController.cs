using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgendaCON.Models;
using AgendaDAL.Models;
using AgendaDAL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AgendaDAL.Controllers
{
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private UserRepository Repository { get; }

        public UserController(AgendaDbContext context)
        {
            Repository = new UserRepository(context);
        }

        // Authenticate the user
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]UserDto user)
        {
            try
            {
                if (user != null)
                {
                    var item = Repository.Authenticate(user.Email, user.PasswordHash);
                    if (item != null)
                    {
                        return Ok(item);
                    }
                    return BadRequest();
                }
                return BadRequest();
            }
            catch
            {
                return BadRequest();
            }
        }

        // GET: api/<controller>
        // Get all users
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
        // Get a user
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
        // Add new user
        [HttpPost]
        public IActionResult Post([FromBody]UserDto item)
        {
            try
            {
                if (item != null)
                {
                    if (Repository.AddItem(item))
                    {
                        return Ok();
                    }
                    return BadRequest();
                }
                return BadRequest();
            }
            catch
            {
                return BadRequest();
            }
        }

        // PUT api/<controller>/5
        // Update a user
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]UserDto item)
        {
            try
            {
                if (item != null)
                {
                    if (Repository.UpdateItem(item))
                    {
                        return Ok();
                    }
                    return BadRequest();
                }
                return BadRequest();
            }
            catch
            {
                return BadRequest();
            }
        }

        // DELETE api/<controller>/5
        // Delete a user
        [HttpDelete("{id}")]
        public IActionResult Delete([FromBody] UserDto item)
        {
            try
            {
                if (item != null)
                {
                    if (Repository.DeleteItem(item))
                    {
                        return Ok();
                    }
                    return BadRequest();
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
