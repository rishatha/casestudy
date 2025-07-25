﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace CareerConnect.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required(ErrorMessage = "UserName is required")]
        [MaxLength(100)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Role is required")]
        public string Role { get; set; } // "jobseeker" or "employer"

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Navigation
        public Employer Employer { get; set; }
        public JobSeeker JobSeeker { get; set; }
    }
}
