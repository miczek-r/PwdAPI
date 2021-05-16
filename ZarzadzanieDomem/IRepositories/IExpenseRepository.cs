using System.Collections.Generic;
using ZarzadzanieDomem.Models;

namespace ZarzadzanieDomem.IRepositories
{
    public interface IExpenseRepository
    {
        public void Create(Expense expense);
        public Expense GetById(uint Id);
        public IEnumerable<Expense> GetAll();
        public void Update(Expense expense, Expense changedExpense);
        public void Delete(Expense expense);
        public void Delete(TypeOfExpense typeOfExpense);
        public void Save();
        public IEnumerable<Expense> GetByUserId(uint id);
        public IEnumerable<Expense> FilterByType(IEnumerable<Expense> expenses, uint typeOfExpenseId);
        public IEnumerable<TypeOfExpense> GetAllExpenseTypes();
        public TypeOfExpense GetExpenseType(uint Id);
        public void CreateTypeOfExpense(TypeOfExpense typeOfExpense);
    }
}
