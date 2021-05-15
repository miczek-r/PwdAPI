using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ZarzadzanieDomem.Models
{
    [Table("Users")]
    public class User
    {
        [Key]
        public uint UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? DateOfBirth { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public decimal Saldo { get; set; } = 0;
        public uint? HomeId { get; set; } = null;
        public string? ActivationToken { get; set; }
        public string? PasswordRestorationToken { get; set; }
        public decimal? ExpenseLimit { get; set; }
    }
}
