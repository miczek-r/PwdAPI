using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ZarzadzanieDomem.Models
{
    public class Notification
    {
        [Key]
        public int NotificationId { get; set; }
        [Required]
        public int RecieverId { get; set; }
        [Required]
        public string Text { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime NotificationDate { get; set; }
        public bool Read { get; set; } = false;
        public string Sender { get; set; }
        public int? HomeId { get; set; }
    }
}
