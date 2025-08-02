using CareerConnect.DTOs;

public interface IEmployerRepository
{
    Task<EmployerDTO> CreateEmployerAsync(EmployerDTO employerDto);
    Task<EmployerDTO> GetEmployerByIdAsync(int id);
    Task<IEnumerable<EmployerDTO>> GetAllEmployersAsync();
    Task<EmployerDTO> UpdateEmployerAsync(int id, EmployerDTO employerDto);
    Task<bool> DeleteEmployerAsync(int id);

    // frontend
    Task<EmployerDTO> GetEmployerByUserIdAsync(int userId);
}
