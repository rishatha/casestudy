namespace CareerConnect.DTOs
{
    public class UserDTO
    {
        public int Id { get; set; }            // maps to UserId
        public string Name { get; set; }       // maps to UserName
        public string Email { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }      // JWT token
    }
}

