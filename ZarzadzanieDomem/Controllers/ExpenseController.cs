using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using ZarzadzanieDomem.IRepositories;
using ZarzadzanieDomem.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ZarzadzanieDomem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseController : ControllerBase
    {
        private readonly IExpenseRepository _expenseRepository;
        private readonly IUserRepository _userRepository;
        private readonly IHomeRepository _homeRepository;
        public ExpenseController(IExpenseRepository expenseRepository, IUserRepository userRepository, IHomeRepository homeRepository)
        {
            _expenseRepository = expenseRepository;
            _userRepository = userRepository;
            _homeRepository = homeRepository;
        }


        [HttpGet]
        public IActionResult GetAll()
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
        public IActionResult Get(uint id)
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
            int temp = DateTime.Compare(expense.ExpenseDate, DateTime.Now);
            if (temp <= 0)
            {
                expense.Accounted = true;
            }
            else if (temp > 0)
            {
                expense.Accounted = false;
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
            return CreatedAtRoute("GetExpense", new { Id = typeOfExpense.TypeOfExpenseId }, typeOfExpense);
        }


        [HttpPut]

        public IActionResult Put(uint id, [FromBody] Expense expense)
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
        public ActionResult Delete(uint id)
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

        [HttpDelete("DeleteTypeOfExpense/{id}")]
        public ActionResult DeleteTypeOfExpense(uint id)
        {
            TypeOfExpense typeOfExpense = _expenseRepository.GetExpenseType(id);
            if (typeOfExpense == null)
            {
                return NotFound("Type of expense not found");
            }
            _expenseRepository.Delete(typeOfExpense);
            _expenseRepository.Save();
            return NoContent();

        }
        [HttpGet("UserId/{UserId}", Name = "GetAllUserExpenses")]
        public IActionResult GetExpensesById(uint UserId)
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

        [HttpGet("HomeId/{HomeId}", Name = "GetHomeExpenses")]
        public IActionResult GetHomeExpenses(uint HomeId)
        {
            if (_homeRepository.GetById(HomeId) == null)
            {
                return NotFound("User not found");
            }
            IEnumerable<Expense> expenses = new List<Expense>();
            IEnumerable<User> users = _userRepository.GetByHomeId(HomeId).ToList();

            foreach (User user in users)
            {
                IEnumerable<Expense> temp = _expenseRepository.GetByUserId(user.UserId).ToList();
                expenses = expenses.Concat(temp);
            }



            if (expenses == null)
            {
                return NotFound("Home has no expenses");
            }
            return Ok(expenses);
        }

        [HttpGet("ByTypeAndUser/{TypeId}/{UserId}", Name = "GetAllUserExpensesByType")]
        public IActionResult GetExpensesByType(uint TypeId, uint UserId)
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
            IEnumerable<Expense> FilteredByType = _expenseRepository.FilterByType(expenses, TypeId);
            return Ok(FilteredByType);
        }
    }
}