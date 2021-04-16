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
        private IUserRepository userRepository;

        public UserController(DatabaseContext context)
        {
            userRepository = new UserRepository(context);
        }

        // GET: api/<UserController>
        [HttpGet]
        public IEnumerable<User> Get()
        {
            return userRepository.GetUsers();
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public ActionResult<User> Get(int id)
        {
            try
            {
                return Ok(userRepository.GetUserById(id));
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        // POST api/<UserController>
        [HttpPost]
        public ActionResult Post([FromBody] User value)
        {
            try
            {
                userRepository.AddUser(value);
                userRepository.Save();
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        // PUT api/<UserController>
        [HttpPut]
        public ActionResult Put([FromBody] User value)
        {
            try
            {
                userRepository.Update(value);
                userRepository.Save();
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                userRepository.Delete(id);
                userRepository.Save();
                return Ok();
            }
            catch(Exception ex)
            {
                return NotFound();
            }
        }
    }
}
