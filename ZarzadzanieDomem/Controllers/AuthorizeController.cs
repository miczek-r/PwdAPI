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
    public class AuthorizeController : ControllerBase
    {
        private IAuthorizeRepository authorizeRepository;

        public AuthorizeController(DatabaseContext context)
        {
            authorizeRepository = new AuthorizeRepository(context);
        }

        [HttpGet("{email} {password}")]
        public User Login(string email, string password)
        {
            User temp = authorizeRepository.GetUserByEmail(email, password);
            if (temp != null) return temp; // TODO: dodac bledy
            return temp;
            
        }


    }
}