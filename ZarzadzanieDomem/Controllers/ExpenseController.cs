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
        public IExpenseRepository _expenseRepository;
        public IUserRepository _userRepository;
        public ExpenseController(IExpenseRepository expenseRepository, IUserRepository userRepository)
        {
            _expenseRepository = expenseRepository;
            _userRepository = userRepository;
        }

        
        [HttpGet]
        public  IActionResult GetAll()
        {
            IEnumerable<Expense> expenses = _expenseRepository.GetAll();
            return Ok(expenses);
        }
        [HttpGet("GetAllExpenseTypes")]
        public IActionResult GetAllExpenseTypes()
        {
            IEnumerable<TypeOfExpense> typesOfExpenses = _expenseRepository.GetAllExpenseTypes();
            return Ok(typesOfExpenses);
        }
        [HttpGet("{id}", Name = "GetExpense")]
        public IActionResult Get(int id)
        {
            Expense expense = _expenseRepository.GetById(id);
            if (expense == null)
            {
                return NotFound("Expense not found");
            }
            return Ok(expense);

        }
        [HttpPost]
        public IActionResult PostExpense([FromBody] Expense expense)
        {
            if (expense == null)
            {
                return BadRequest("Expense is empty");
            }
            _expenseRepository.Create(expense);
            _expenseRepository.Save();
            return CreatedAtRoute("GetExpense", new { Id = expense.ExpenseId }, expense);
        }
        [HttpPost("TypeOfExpense")]
        public IActionResult PostTypeOfExpense([FromBody] TypeOfExpense typeOfExpense)
        {
            if (typeOfExpense == null)
            {
                return BadRequest("typeOfExpense is empty");
            }
            _expenseRepository.CreateTypeOfExpense(typeOfExpense);
            _expenseRepository.Save();
            return CreatedAtRoute("GetExpense", new { Id = typeOfExpense.TypeOfExpenseId}, typeOfExpense);
        }


        [HttpPut]

        public IActionResult Put(int id, [FromBody] Expense expense)
        {
            if (expense == null)
            {
                return BadRequest("Expense is empty");
            }
            Expense expenseToUpdate = _expenseRepository.GetById(id);
            if (expenseToUpdate == null)
            {
                return NotFound("Expense not found");
            }
            _expenseRepository.Update(expenseToUpdate, expense);
            _expenseRepository.Save();
            return NoContent();
            
        }
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
           Expense expense = _expenseRepository.GetById(id);
            if (expense == null)
            {
                return NotFound("Expense not found");
            }
            _expenseRepository.Delete(expense);
            _expenseRepository.Save();
            return NoContent();
            
        }
        [HttpGet("UserId/{UserId}", Name = "GetAllUserExpenses")]
        public IActionResult GetExpensesById(int UserId)
        {
            User user = _userRepository.GetById(UserId);
            if (user == null)
            {
                return NotFound("User not found");
            }
            if (_expenseRepository.GetByUserId(UserId) == null)
            {
                return NotFound("User has no expenses");
            }
            IEnumerable<Expense> expenses = _expenseRepository.GetByUserId(UserId);
            return Ok(expenses);
        }
        [HttpGet("ByTypeAndUser/{TypeId}/{UserId}", Name = "GetAllUserExpensesByType")]
        public IActionResult GetExpensesByType(int TypeId,int UserId)
        {
            User user = _userRepository.GetById(UserId);
            if (user == null)
            {
                return NotFound("User not found");
            }
            if (_expenseRepository.GetByUserId(UserId) == null)
            {
                return NotFound("User has no expenses");
            }
            IEnumerable<Expense> expenses = _expenseRepository.GetByUserId(UserId);
            IEnumerable<Expense> FilteredByType = _expenseRepository.FilterByType(expenses,TypeId);
            return Ok(FilteredByType);
        }
    }
}