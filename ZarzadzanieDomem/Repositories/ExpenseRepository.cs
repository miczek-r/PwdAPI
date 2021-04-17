using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using ZarzadzanieDomem.IRepositories;
using ZarzadzanieDomem.Models;
using ZarzadzanieDomem.Models.Context;

namespace ZarzadzanieDomem.Repositories
{
    public class ExpenseRepository : IExpenseRepository
    {
        private readonly DatabaseContext _context;
        public ExpenseRepository(DatabaseContext context)
        {
            _context = context;
        }
        public void AddExpense(Expense expense)
        {
            _context.Expenses.Add(expense);
        }
        public Expense FindExpense(int Id)
        {

            if (_context.Expenses.Find(Id) != null)
            {
                return _context.Expenses.Where(expense => expense.ExpenseId == Id).FirstOrDefault();
            }
            else
            {
                throw new Exception();
            }
            
        }
        public IEnumerable<Expense> GetExpenses()
        {
            return _context.Expenses.ToList();
        }
        public void Save()
        {
            _context.SaveChanges();
        }
        public void Update(Expense value)
        {
            Expense expense = _context.Expenses.Where(expense => expense.ExpenseId == value.ExpenseId).FirstOrDefault();
            if (expense != null)
            {
                expense.Amount = value.Amount;
                expense.ExpenseId = value.ExpenseId;
                expense.NameOfExpense = value.NameOfExpense;
                _context.Expenses.Update(expense);
            }
            else
            {
                throw new Exception();
            }
        }
        public void Delete(int id)
        {

            if (_context.Expenses.Find(id)!=null)
            {
                _context.Expenses.Remove(_context.Expenses.Find(id));
            }
            else
            {
                throw new Exception();
            }
        }
    }
}

