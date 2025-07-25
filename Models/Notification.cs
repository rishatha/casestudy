using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CareerConnect.Models
{
    public class Notification
    {
        [Key]
        public int NotificationId { get; set; }

        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Message is required")]
        [MaxLength(250)]
        public string Message { get; set; }

        public bool IsRead { get; set; } = false;

        public DateTime SentAt { get; set; } = DateTime.Now;

        //  Navigation property
        public User User { get; set; }
    }
}
