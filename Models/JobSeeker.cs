using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CareerConnect.Models
{
    public class JobSeeker
    {
        [Key]
        public int JobSeekerID { get; set; }

        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "First name is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [Phone(ErrorMessage = "Invalid phone number")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Qualification is required")]
        public string Qualification { get; set; }

        [Required(ErrorMessage = "Skills are required")]
        public string Skills { get; set; }

        // Navigation
        public User User { get; set; }
        public ICollection<Application> Applications { get; set; }
        public Resume Resume { get; set; }
    }
}
