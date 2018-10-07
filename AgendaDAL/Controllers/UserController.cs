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
        private UserService Service { get; }

        public UserController(AgendaDbContext context)
        {
            Service = new UserService(context);
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]UserDto userParam)
        {
            var user = Service.Authenticate(userParam.Email, userParam.PasswordHash);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(user);
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
        public IActionResult Post([FromBody]UserDto item)
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
        public IActionResult Put(int id, [FromBody]UserDto item)
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
        public IActionResult Delete([FromBody] UserDto item)
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
