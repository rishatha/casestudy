using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CareerConnect.Models
{
    public class Job
    {
        [Key]
        public int JobId { get; set; }

        [Required]
        [ForeignKey("Employer")]
        public int EmployerId { get; set; }

        [Required(ErrorMessage = "Job title is required")]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required(ErrorMessage = "Job description is required")]
        [MaxLength(1000)]
        public string Description { get; set; }

        [Required(ErrorMessage = "Qualifications are required")]
        public string Qualifications { get; set; }

        [Required(ErrorMessage = "Location is required")]
        [MaxLength(100)]
        public string Location { get; set; }

        [Required(ErrorMessage = "Salary is required")]
        public decimal Salary { get; set; }

        [Required(ErrorMessage = "Company name is required")]
        [MaxLength(100)]
        public string CompanyName { get; set; }

        public DateTime PostedDate { get; set; } = DateTime.Now;

        public bool IsActive { get; set; } = true;

        public Employer Employer { get; set; }
        public ICollection<Application> Applications { get; set; }
    }
}
