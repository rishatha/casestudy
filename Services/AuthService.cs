using CareerConnect.Data;
using CareerConnect.DTOs;
using CareerConnect.Interfaces;
using CareerConnect.Models;
using System.Security.Cryptography;
using System.Text;

namespace CareerConnect.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;

        public AuthService(AppDbContext context)
        {
            _context = context;
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(bytes);
            }
        }

        public string Register(RegisterDTO dto)
        {
            if (_context.Users.Any(u => u.Email == dto.Email))
                return "Email already exists.";

            var user = new User
            {
                UserName = dto.UserName,
                Email = dto.Email,
                Password = HashPassword(dto.Password),
                Role = dto.Role,
                CreatedAt = DateTime.Now
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            return "Registration successful.";
        }

        public string Login(LoginDTO dto)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == dto.Email);
            if (user == null)
                return "Invalid email or password.";

            string hashedInputPassword = HashPassword(dto.Password);

            if (user.Password != hashedInputPassword)
                return "Invalid email or password.";

            return "Login successful.";
        }
    }
}
