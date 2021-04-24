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
        public void Create(Expense expense)
        {
            _context.Expenses.Add(expense);
        }
        public Expense GetById(int Id)=> _context.Expenses.FirstOrDefault(e => e.ExpenseId == Id);
        
        public IEnumerable<Expense> GetAll()
        {
            return _context.Expenses.ToList();
        }
        public void Save()
        {
            _context.SaveChanges();
        }
        public void Update(Expense expense, Expense changedExpense)
        {
                expense.Amount = changedExpense.Amount;
                expense.ExpenseId = changedExpense.ExpenseId;
                expense.NameOfExpense = changedExpense.NameOfExpense;
                _context.Expenses.Update(expense);
        }
        public void Delete(Expense expense)
        {
            _context.Expenses.Remove(expense);
        }
        public IEnumerable<Expense> GetByUserId(int id) => _context.Expenses.Where(e => e.OwnerId == id).ToList();

        public IEnumerable<Expense> SortByType(IEnumerable<Expense> expenses,int typeOfExpenseId)
        {
            List<Expense> list = new List<Expense>();
            foreach (var element in expenses)
            {
                if (element.TypeOfExpenseId == typeOfExpenseId)
                {
                    list.Add(element);
                }
            }
            IEnumerable<Expense> SortedByType = list;
            return SortedByType;
        }
        public IEnumerable<TypeOfExpense> GetAllExpenseTypes()
        {
            return _context.TypesOfExpenses.ToList();
        }
        public void CreateTypeOfExpense(TypeOfExpense typeOfExpense)
        {
            _context.TypesOfExpenses.Add(typeOfExpense);
        }
    }
}

