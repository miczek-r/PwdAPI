using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZarzadzanieDomem.Models
{
    [Table("Expenses")]
    public class Expense
    {
        [Key]
        public uint ExpenseId { get; set; }
        [Required]
        public string NameOfExpense { get; set; }
        [Required]
        public decimal Amount { get; set; }
        [DataType(DataType.Date)]
        [Column(TypeName = "date")]
        [DisplayFormat(DataFormatString = "{yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ExpenseDate { get; set; }
        [Required]
        public uint TypeOfExpenseId { get; set; }
        public uint OwnerId { get; set; }
        public bool Accounted { get; set; }
    }
}
