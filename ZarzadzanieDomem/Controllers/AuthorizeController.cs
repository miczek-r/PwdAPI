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
        private IAuthorizeRepository authorizeRepository;

        public AuthorizeController(DatabaseContext context)
        {
            authorizeRepository = new AuthorizeRepository(context);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(User))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Login([FromBody]Auth auth)
        {
            User user = authorizeRepository.GetUserByEmail(auth);
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


    }
}