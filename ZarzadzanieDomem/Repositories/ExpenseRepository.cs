using System.Collections.Generic;
using System.Linq;
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
        public Expense GetById(uint Id) => _context.Expenses.FirstOrDefault(e => e.ExpenseId == Id);

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
        public void Delete(TypeOfExpense typeOfExpense) => _context.TypesOfExpenses.Remove(typeOfExpense);
        public IEnumerable<Expense> GetByUserId(uint id) => _context.Expenses.Where(e => e.OwnerId == id).ToList();



        public IEnumerable<Expense> FilterByType(IEnumerable<Expense> expenses, uint typeOfExpenseId)
        {
            List<Expense> list = new List<Expense>();
            foreach (var element in expenses)
            {
                if (element.TypeOfExpenseId == typeOfExpenseId)
                {
                    list.Add(element);
                }
            }
            IEnumerable<Expense> FilteredByType = list;
            return FilteredByType;
        }
        public IEnumerable<TypeOfExpense> GetAllExpenseTypes() => _context.TypesOfExpenses.ToList();
        public TypeOfExpense GetExpenseType(uint Id) => _context.TypesOfExpenses.FirstOrDefault(e => e.TypeOfExpenseId == Id);

        public void CreateTypeOfExpense(TypeOfExpense typeOfExpense) => _context.TypesOfExpenses.Add(typeOfExpense);
    }
}

