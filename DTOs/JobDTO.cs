using System;
using System.ComponentModel.DataAnnotations;

namespace CareerConnect.DTOs
{
    public class JobDTO
    {
        public int JobId { get; set; }

        [Required(ErrorMessage = "EmployerId is required")]
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

        public DateTime PostedAt { get; set; } = DateTime.Now;
        public bool IsActive { get; set; } = true;
    }
}
