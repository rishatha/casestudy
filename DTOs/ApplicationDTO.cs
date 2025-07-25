using System;
using System.ComponentModel.DataAnnotations;

namespace CareerConnect.DTOs
{
    public class ApplicationDTO
    {
        public int ApplicationId { get; set; }

        [Required(ErrorMessage = "JobId is required")]
        public int JobId { get; set; }

        [Required(ErrorMessage = "JobSeekerId is required")]
        public int JobSeekerId { get; set; }

        [Required(ErrorMessage = "Status is required")]
        [MaxLength(50)]
        public string Status { get; set; } = "Applied";

        public DateTime AppliedAt { get; set; } = DateTime.Now;
    }
}
