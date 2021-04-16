﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ZarzadzanieDomem.Models
{
    [Table("TypesOfExpenses")]
    public class TypeOfExpense
    {
        [Key]
        public int TypeOfExpenseId { get; set; }
        public string Name { get; set; }

    }
}