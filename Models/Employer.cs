using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CareerConnect.Models
{
    public class Employer
    {
        [Key]
        public int EmployerId { get; set; }

        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "First name is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [Phone(ErrorMessage = "Invalid phone number")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Company name is required")]
        [MaxLength(100)]
        public string CompanyName { get; set; }

        public string Website { get; set; }

        public bool IsActive { get; set; } = true;

        // Navigation
        public User User { get; set; }
        public ICollection<Job> Jobs { get; set; }
    }
}
