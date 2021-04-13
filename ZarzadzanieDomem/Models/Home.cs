using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ZarzadzanieDomem.Models
{
    [Table("Homes")]
    public class Home
    {
        [Key]
        public int HomeId { get; set; }
        public string HomeName { get; set; }
        public string Adress { get; set; }
        public List<User> Users { get; set; }
    }
}
