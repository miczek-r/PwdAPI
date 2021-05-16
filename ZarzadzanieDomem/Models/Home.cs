using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZarzadzanieDomem.Models
{
    [Table("Homes")]
    public class Home
    {
        [Key]
        public uint HomeId { get; set; }
        [Required]
        public string HomeName { get; set; }
#nullable enable
        public string? Street { get; set; }
        public string? HouseNumber { get; set; }
        public string? PostCode { get; set; }
        public string? City { get; set; }
#nullable disable
    }
}
