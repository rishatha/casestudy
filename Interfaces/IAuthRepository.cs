using CareerConnect.DTOs;
using CareerConnect.Models;

public interface IAuthRepository
{
    string Register(RegisterDTO dto);
    User Login(LoginDTO dto);
    User GetUserByRefreshToken(string refreshToken);
    string DeleteUser(int userId);
}
