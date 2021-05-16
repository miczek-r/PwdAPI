using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZarzadzanieDomem.Models
{
    public class Notification
    {
        [Key]
        public uint NotificationId { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string ReceiverEmail { get; set; }
        [Required]
        public string Text { get; set; }
        [DataType(DataType.Date)]
        [Column(TypeName = "date")]
        [DisplayFormat(DataFormatString = "{yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime NotificationDate { get; set; }
        public bool Read { get; set; } = false;
        public string Sender { get; set; }
        public uint? HomeId { get; set; }
    }
}
