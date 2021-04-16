using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZarzadzanieDomem.Models;

namespace ZarzadzanieDomem.IRepositories
{
    interface IExpenseRepository
    {
        public void AddExpense(Expense expense);
        public Expense FindExpense(int Id);
        public IEnumerable<Expense> GetExpenses();
        public void Update(Expense value);
        public void Delete(int id);
        public void Save();
    }
}
