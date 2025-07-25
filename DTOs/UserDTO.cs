using System.ComponentModel.DataAnnotations;

namespace CareerConnect.DTOs
{
    public class UserDTO
    {
        public int Id { get; set; }  // maps to UserId

        [Required(ErrorMessage = "User name is required.")]
        [MaxLength(100, ErrorMessage = "User name cannot exceed 100 characters.")]
        public string UserName { get; set; }  
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Role is required.")]
        public string Role { get; set; }  // jobseeker / employer

        public string Token { get; set; }  // only returned after login
    }
}


