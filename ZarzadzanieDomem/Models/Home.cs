﻿using System;
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
        [Required]
        public string HomeName { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public string PostCode { get; set; }
        public string City { get; set; }
        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<Expense> Expenses { get; set; }
    }
}
