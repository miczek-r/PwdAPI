using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZarzadzanieDomem.Authentication;
using ZarzadzanieDomem.IRepositories;
using ZarzadzanieDomem.Models;
using ZarzadzanieDomem.Models.Context;
using ZarzadzanieDomem.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ZarzadzanieDomem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizeController : ControllerBase
    {
        private IAuthorizeRepository _authorizeRepository;
        private IUserRepository _userRepository;

        public AuthorizeController(IUserRepository userRepository, IAuthorizeRepository authorizeRepository)
        {
            _userRepository = userRepository;
            _authorizeRepository = authorizeRepository;
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(User))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Login([FromBody]Auth auth)
        {
            User user = _authorizeRepository.GetUserByEmail(auth);
            if (user == null )
            {
                return NotFound("User not found");
            }
            if (user.ActivationToken != null)
            {
                return BadRequest("User is not activated");
            }
            return Ok(user);
        }
        [HttpPut("RestorePassword/{token}/{password}")]
        public IActionResult RestorePassword(string token, string password)
        {
            if (token == null)
            {
                return BadRequest("token is empty.");
            }
            User user = _authorizeRepository.GetUserByRestorationToken(token);
            if (user == null)
            {
                return BadRequest("There is no user with that token");
            }
            user.PasswordRestorationToken = null;
            user.Password = _authorizeRepository.EncodePassword(password);
            User userToUpdate = _userRepository.GetById(user.UserId);
            _userRepository.Update(userToUpdate, user);
            _userRepository.Save();
            return NoContent();
        }
        [HttpPut("GetPasswordRestorationToken/{email}")]
        public IActionResult GetPasswordRestorationToken(string email)
        {
            if (email == null)
            {
                return BadRequest("Email is empty.");
            }
            User user = _userRepository.GetUserByEmail(email);
            if (user == null)
            {
                return BadRequest("There is no user with that email");
            }
            string token = user.UserId.ToString() + _authorizeRepository.TokenGenerator(user);
            user.PasswordRestorationToken = token;
            User userToUpdate = _userRepository.GetById(user.UserId);
            _userRepository.Update(userToUpdate, user);
            _userRepository.Save();
            token = "http://188.137.40.31/restore/" + token;
            _authorizeRepository.SendRestorationEmail(user, token);
            return NoContent();
        }

        [HttpPut("ConfirmEmail/{token}")]
        public IActionResult ConfirmEmail(string token)
        {
            if (token == null)
            {
                return BadRequest("Email is empty.");
            }
            User user = _authorizeRepository.GetUserByActivationToken(token);
            if (user == null)
            {
                return BadRequest("There is no user with that email");
            }
            user.ActivationToken = null;
            User userToUpdate = _userRepository.GetById(user.UserId);
            _userRepository.Update(userToUpdate, user);
            _userRepository.Save();
            return NoContent();
        }

    }
}