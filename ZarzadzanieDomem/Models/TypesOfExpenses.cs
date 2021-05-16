using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZarzadzanieDomem.Models
{
    [Table("TypesOfExpenses")]
    public class TypeOfExpense
    {
        [Key]
        public uint TypeOfExpenseId { get; set; }
        [Required]
        public string Name { get; set; }

    }
}
