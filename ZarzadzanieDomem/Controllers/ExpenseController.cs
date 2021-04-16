using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZarzadzanieDomem.IRepositories;
using ZarzadzanieDomem.Models;
using ZarzadzanieDomem.Models.Context;
using ZarzadzanieDomem.Repositories;
using Microsoft.AspNetCore.Http;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ZarzadzanieDomem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseController : ControllerBase
    {
        private IExpenseRepository expenseRepository;

        public ExpenseController(DatabaseContext context)
        {
            expenseRepository = new ExpenseRepository(context);
        }

        [HttpPost]
        public void Post([FromBody] Expense value)
        {
            expenseRepository.AddExpense(value);
            expenseRepository.Save();
        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ActionResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(object))]
        [ProducesDefaultResponseType]
        public ActionResult<Expense> GetExpenseById(int id)
        {
            try
            {
                return Ok(expenseRepository.FindExpense(id));
            }catch(Exception ex)
            {
                return NotFound();
            }
            
        }
        [HttpGet]
        public IEnumerable<Expense> GetAll()
        {
            return expenseRepository.GetExpenses();
        }
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ActionResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(object))]
        [ProducesDefaultResponseType]
        public ActionResult Put([FromBody] Expense value)
        {
            try
            {
                expenseRepository.Update(value);
                expenseRepository.Save();
                return Ok();
            }catch(Exception ex)
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
                expenseRepository.Delete(id);
                expenseRepository.Save();
                return Ok();
            }catch(Exception ex)
            {
                return NotFound();
            }
            
        }


    }
}