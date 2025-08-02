using CareerConnect.Data;
using CareerConnect.DTOs;
using CareerConnect.Exceptions;
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
            if (string.IsNullOrWhiteSpace(dto.Email) || string.IsNullOrWhiteSpace(dto.Password))
                throw new ValidationException("Email and password are required.");

            if (_context.Users.Any(u => u.Email == dto.Email && u.IsActive))
                throw new BadRequestException("Email already exists.");

            //  Always hash the incoming password
            var hashedPassword = HashPassword(dto.Password);

            var user = new User
            {
                UserName = dto.UserName,
                Email = dto.Email,
                Password = hashedPassword,
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
             if (string.IsNullOrWhiteSpace(dto.Email) || string.IsNullOrWhiteSpace(dto.Password))
                 throw new ValidationException("Email and password are required.");

             var user = _context.Users.FirstOrDefault(u => u.Email == dto.Email && u.IsActive);
             if (user == null)
                 throw new BadRequestException("Invalid email or password.");

             //  Always hash the incoming password before comparison
             var hashedPassword = HashPassword(dto.Password);
             if (user.Password != hashedPassword)
                 throw new BadRequestException("Invalid email or password.");

             // Generate refresh token
             user.RefreshToken = GenerateRefreshToken();
             user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

             _context.SaveChanges();
             return user;
         }
        
    

        public User GetUserByRefreshToken(string refreshToken)
        {
            var user = _context.Users.FirstOrDefault(u => u.RefreshToken == refreshToken && u.RefreshTokenExpiryTime > DateTime.UtcNow);

            if (user == null)
                throw new NotFoundException("User with given refresh token not found or expired.");

           return user;

        }

        public string DeleteUser(int userId)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserId == userId);
            if (user == null)
                throw new NotFoundException("User not found.");

            user.IsActive = false;
            _context.SaveChanges();

            return "User deactivated.";
        }
    }
}
