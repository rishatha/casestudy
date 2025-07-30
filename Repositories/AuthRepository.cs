using CareerConnect.Data;
using CareerConnect.DTOs;
using CareerConnect.Interfaces;
using CareerConnect.Models;
using System.Security.Cryptography;
using System.Text;

namespace CareerConnect.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly AppDbContext _context;

        public AuthRepository(AppDbContext context)
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

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        public string Register(RegisterDTO dto)
        {
            if (_context.Users.Any(u => u.Email == dto.Email && u.IsActive))
                return "Email already exists.";

            var user = new User
            {
                UserName = dto.UserName,
                Email = dto.Email,
                Password = HashPassword(dto.Password),
                Role = dto.Role,
                CreatedAt = DateTime.Now,
                IsActive = true
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            return "Registration successful.";
        }

        public User Login(LoginDTO dto)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == dto.Email && u.IsActive);
            if (user == null || user.Password != HashPassword(dto.Password))
                return null;

            // Generate refresh token
            user.RefreshToken = GenerateRefreshToken();
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

            _context.SaveChanges();
            return user;
        }

        public User GetUserByRefreshToken(string refreshToken)
        {
            return _context.Users.FirstOrDefault(u => u.RefreshToken == refreshToken && u.RefreshTokenExpiryTime > DateTime.UtcNow);
        }

        public string DeleteUser(int userId)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserId == userId);
            if (user == null)
                return "User not found.";

            user.IsActive = false;
            _context.SaveChanges();

            return "User deactivated.";
        }
    }
}
