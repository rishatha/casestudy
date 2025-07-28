using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CareerConnect.Models
{
    public class Application
    {
        [Key]
        public int ApplicationId { get; set; }

        [Required]
        [ForeignKey("Job")]
        public int JobId { get; set; }

        [Required]
        [ForeignKey("JobSeeker")]
        public int JobSeekerId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Status { get; set; } = "Applied";

        public DateTime AppliedAt { get; set; } = DateTime.Now;

        public bool IsActive { get; set; } = true; // Added for soft delete

        // Navigation
        public Job Job { get; set; }
        public JobSeeker JobSeeker { get; set; }
    }
}
