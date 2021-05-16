using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZarzadzanieDomem.Models
{
    [Table("Users")]
    public class User
    {
        [Key]
        public uint UserId { get; set; }
#nullable enable
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
#nullable disable
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Column(TypeName = "date")]
        public DateTime? DateOfBirth { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public decimal Saldo { get; set; } = 0;
        public uint? HomeId { get; set; } = null;
#nullable enable
        public string? ActivationToken { get; set; }
        public string? PasswordRestorationToken { get; set; }
#nullable disable
        public decimal? ExpenseLimit { get; set; }
    }
}
