using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using ZarzadzanieDomem.Authentication;
using ZarzadzanieDomem.IRepositories;
using ZarzadzanieDomem.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ZarzadzanieDomem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizeController : ControllerBase
    {
        private readonly IAuthorizeRepository _authorizeRepository;
        private readonly IUserRepository _userRepository;
        private readonly IExpenseRepository _expenseRepository;

        public AuthorizeController(IUserRepository userRepository, IAuthorizeRepository authorizeRepository, IExpenseRepository expenseRepository)
        {
            _userRepository = userRepository;
            _authorizeRepository = authorizeRepository;
            _expenseRepository = expenseRepository;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(User))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Login([FromBody] Auth auth)
        {
            try
            {
                User user = _authorizeRepository.GetUserByEmail(auth);
                if (user == null)
                {
                    return NotFound("User not found");
                }
                if (user.ActivationToken != null)
                {
                    return BadRequest("User is not activated");
                }
                IEnumerable<Expense> expenses = _expenseRepository.GetByUserId(user.UserId);
                User userToUpdate = user;
                foreach (Expense expense in expenses)
                {
                    int temp = DateTime.Compare(expense.ExpenseDate, DateTime.Now);
                    if (temp <= 0 && expense.Accounted==false)
                    {
                        expense.Accounted = true;
                        user.Saldo += (expense.TypeOfExpenseId == 1) ? expense.Amount : -(expense.Amount);
                    }
                }
                _userRepository.Update(userToUpdate, user);
                _expenseRepository.Save();
                return Ok(user);
            }
            catch (UnauthorizedAccessException)
            {
                return BadRequest("Wrong password or email");
            }


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