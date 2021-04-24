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
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ExpenseDate { get; set; }
        [Required]
        public int TypeOfExpenseId { get; set; }
        public int OwnerId { get; set; }
    }
}
