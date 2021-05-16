using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using ZarzadzanieDomem.IRepositories;
using ZarzadzanieDomem.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ZarzadzanieDomem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthorizeRepository _authorizeRepository;
        private readonly IHomeRepository _homeRepository;

        public UserController(IUserRepository userRepository, IAuthorizeRepository authorizeRepository, IHomeRepository homeRepository)
        {
            _userRepository = userRepository;
            _authorizeRepository = authorizeRepository;
            _homeRepository = homeRepository;
        }

        // GET: api/<UserController>
        [HttpGet]
        public IActionResult GetAll()
        {
            IEnumerable<User> users = _userRepository.GetAll();
            return Ok(users);
        }

        // GET api/<UserController>/5
        [HttpGet("{id}", Name = "GetUser")]
        public IActionResult Get(uint id)
        {
            User user = _userRepository.GetById(id);
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
            string tempPassword = _authorizeRepository.EncodePassword(user.Password);
            user.Password = tempPassword;
            _userRepository.Create(user);
            _userRepository.Save();
            string token = user.UserId.ToString() + _authorizeRepository.TokenGenerator(user);
            user.ActivationToken = token;
            User userToUpdate = _userRepository.GetById(user.UserId);
            _userRepository.Update(userToUpdate, user);
            _userRepository.Save();
            token = "http://188.137.40.31/activate/" + token;
            _authorizeRepository.SendVerificationEmail(user, token);
            return CreatedAtRoute("GetUser", new { Id = user.UserId }, user);
        }

        // PUT api/<UserController>
        [HttpPut]
        public IActionResult Put([FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest("User is empty");
            }
            User userToUpdate = _userRepository.GetById(user.UserId);
            if (userToUpdate == null)
            {
                return NotFound("User not found");
            }
            _userRepository.Update(userToUpdate, user);
            _userRepository.Save();
            return NoContent();
        }


        // PUT api/<UserController>
        [HttpPut("JoinHome/{UserId}/{HomeId}")]
        public IActionResult JoinHome(uint UserId, uint HomeId)
        {
            User user = _userRepository.GetById(UserId);
            if (user == null)
            {
                return BadRequest("User does not exists");
            }
            if (_homeRepository.GetById(HomeId) == null)
            {
                return BadRequest("Home does not exists");
            }
            if (user.HomeId != null)
            {
                return Conflict("User already has a home");
            }

            User userToUpdate = user;
            userToUpdate.HomeId = HomeId;
            _userRepository.Update(userToUpdate, user);
            _userRepository.Save();
            return NoContent();
        }

        // PUT api/<UserController>
        [HttpPut("LeaveHome/{UserId}")]
        public IActionResult LeaveHome(uint UserId)
        {

            User user = _userRepository.GetById(UserId);
            if (user == null)
            {
                return BadRequest("User does not exists");
            }
            uint? HomeId = user.HomeId;
            User userToUpdate = user;
            userToUpdate.HomeId = null;
            _userRepository.Update(userToUpdate, user);
            _userRepository.Save();
            if (HomeId != null && !_userRepository.GetByHomeId(HomeId).Any())
            {
                Home home = _homeRepository.GetById(HomeId);
                _homeRepository.Delete(home);
                _homeRepository.Save();
            }
            return NoContent();
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(uint id)
        {
            User user = _userRepository.GetById(id);
            if (user == null)
            {
                return NotFound("User not found");
            }
            _userRepository.Delete(user);
            _userRepository.Save();
            return NoContent();
        }


    }
}
