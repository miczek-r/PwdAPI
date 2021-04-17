using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ZarzadzanieDomem.Models
{
    [Table("Expenses")]
    public class Expense
    {
        [Key]
        public int ExpenseId { get; set; }
        [Required]
        public string NameOfExpense { get; set; }
        [Required]
        public decimal Amount { get; set; }
        [Required]
        public TypeOfExpense TypeOfExpense { get; set; }
    }
}
