using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZarzadzanieDomem.IRepositories;
using ZarzadzanieDomem.Models;
using ZarzadzanieDomem.Models.Context;
using ZarzadzanieDomem.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ZarzadzanieDomem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // GET: api/<UserController>
        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<User> users = _userRepository.GetUsers();
            return Ok(users);
        }

        // GET api/<UserController>/5
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(int id)
        {
            User user = _userRepository.GetUser(id);
            if (user == null)
            {
                return NotFound("User not found");
            }
            return Ok(user);
        }

        // POST api/<UserController>
        [HttpPost]
        public IActionResult Post([FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest("User is empty.");
            }
            if (_userRepository.GetUserByEmail(user.Email) != null)
            {
                return BadRequest("User already exists");
            }
            
            _userRepository.AddUser(user);
            _userRepository.Save();
            return CreatedAtRoute("Get", new { Id = user.UserId }, user);
        }

        // PUT api/<UserController>
        [HttpPut]
        public IActionResult Put(int id,[FromBody] User user)
        {
           if(user == null)
            {
                return BadRequest("User is empty");
            }
            User userToUpdate = _userRepository.GetUser(id);
            if (userToUpdate == null)
            {
                return NotFound("User not found");
            }
            _userRepository.Update(userToUpdate, user);
            _userRepository.Save();
            return NoContent();
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            User user = _userRepository.GetUser(id);
            if(user == null)
            {
                return NotFound("User not found");
            }
            _userRepository.Delete(user);
            _userRepository.Save();
            return NoContent();
        }
    }
}
