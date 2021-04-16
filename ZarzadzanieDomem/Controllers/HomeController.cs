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
        private IHomeRepository homeRepository;

        public HomeController(DatabaseContext context)
        {
            homeRepository = new HomeRepository(context);
        }

        [HttpPost]
        public void Post([FromBody] Home value)
        {
            homeRepository.AddHome(value);
            homeRepository.Save();
        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ActionResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(object))]
        [ProducesDefaultResponseType]
        public ActionResult<Home> GetHomeById(int id)
        {
            try
            {
                return Ok(homeRepository.GetHomeById(id));
            }
            catch (Exception ex)
            {
                return NotFound();
            }

        }
        [HttpGet]
        public IEnumerable<Home> GetAll()
        {
            return homeRepository.GetHomes();
        }
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ActionResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(object))]
        [ProducesDefaultResponseType]
        public ActionResult Put([FromBody] Home value)
        {
            try
            {
                homeRepository.Update(value);
                homeRepository.Save();
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound();
            }

        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ActionResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(object))]
        [ProducesDefaultResponseType]
        public ActionResult Delete(int id)
        {
            try
            {
                homeRepository.Delete(id);
                homeRepository.Save();
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound();
            }

        }
    }
}
