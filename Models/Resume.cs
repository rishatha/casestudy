using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CareerConnect.Models
{
    public class Resume
    {
        [Key]
        public int ResumeId { get; set; }

        [Required]
        [ForeignKey("JobSeeker")]
        public int JobSeekerId { get; set; }

        [Required(ErrorMessage = "Resume path is required")]
        [MaxLength(255)]
        public string ResumePath { get; set; }

        public DateTime UploadedAt { get; set; } = DateTime.Now;

        // Navigation
        public JobSeeker JobSeeker { get; set; }
    }
}
