using CareerConnect.DTOs;

namespace CareerConnect.Interfaces
{
    public interface IAuthService
    {
        string Register(RegisterDTO dto);
        string Login(LoginDTO dto);
    }
}
