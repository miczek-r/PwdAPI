using Microsoft.AspNetCore.Http;
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
    public class HomeController : ControllerBase
    {
        private readonly IHomeRepository _homeRepository;
        private readonly IUserRepository _userRepository;

        public HomeController(DatabaseContext context)
        {
            _homeRepository = new HomeRepository(context);
            _userRepository = new UserRepository(context);

        }
        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<Home> homes = _homeRepository.GetAll();
            return Ok(homes);
        }

        [HttpGet("{id}", Name = "GetHome")]
        public IActionResult Get(uint id)
        {
            Home home = _homeRepository.GetById(id);
            if (home == null)
            {
                return NotFound("Home not found");
            }
            return Ok(home);

        }


        [HttpGet("UserId/{UserId}", Name = "GetHomeByUserID")]
        public IActionResult GetHomeByID(uint UserId)
        {
            User user = _userRepository.GetById(UserId);
            if (user == null)
            {
                return NotFound("User not found");
            }
            if (user.HomeId == null)
            {
                return NotFound("User has no home");
            }
            Home home = _homeRepository.GetByUser(user);
            return Ok(home);

        }

        [HttpGet("AllHomeUsers/{HomeId}")]
        public IActionResult GetUsersByHomeId(uint HomeId)
        {
            if (_homeRepository.GetById(HomeId) == null)
            {
                return NotFound("This home does not exists");
            }
            IEnumerable<User> users = _userRepository.GetByHomeId(HomeId);
            if (users.Count()==0)
            {
                return NotFound("Home has no users");
            }
            return Ok(users);

        }

        [HttpPost]
        public IActionResult Post([FromBody] Home home)
        {
            if (home == null)
            {
                return BadRequest("Home is empty.");
            }
            _homeRepository.Create(home);
            _homeRepository.Save();
            return CreatedAtRoute("GetHome", new { Id = home.HomeId }, home);
        }
        
        
        [HttpPut]
        public ActionResult Put(uint id,[FromBody] Home home)
        {
            if (home == null)
            {
                return BadRequest("Home is empty");
            }
            Home homeToUpdate = _homeRepository.GetById(id);
            if (homeToUpdate == null)
            {
                return NotFound("Home not found");
            }
            _homeRepository.Update(homeToUpdate,home);
            _homeRepository.Save();
            return NoContent();

        }
        [HttpDelete("{id}")]
        public ActionResult Delete(uint id)
        {
            Home home = _homeRepository.GetById(id);
            if (home == null)
            {
                return NotFound("Home not Found");
            }
            _homeRepository.Delete(home);
            _homeRepository.Save();
            return NoContent();

        }
    }
}
