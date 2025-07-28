using CareerConnect.DTOs;

namespace CareerConnect.Interfaces
{
    public interface IAuthRepository
    {
        string Register(RegisterDTO dto);
        string Login(LoginDTO dto);
        string DeleteUser(int userId); 
    }
}
