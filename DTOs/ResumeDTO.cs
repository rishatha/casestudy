using System;
using System.ComponentModel.DataAnnotations;

namespace CareerConnect.DTOs
{
    public class ResumeDTO
    {
        public int ResumeId { get; set; }

        [Required]
        public int JobSeekerId { get; set; }

        [Required(ErrorMessage = "Resume path is required")]
        [MaxLength(255)]
        public string ResumePath { get; set; }

        public DateTime UploadedAt { get; set; } = DateTime.Now;
    }
}
