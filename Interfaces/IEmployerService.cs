using CareerConnect.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IEmployerService
{
    Task<EmployerDTO> CreateEmployerAsync(EmployerDTO employerDto);
    Task<EmployerDTO> GetEmployerByIdAsync(int id);
    Task<IEnumerable<EmployerDTO>> GetAllEmployersAsync();
    Task<EmployerDTO> UpdateEmployerAsync(int id, EmployerDTO employerDto);
    Task<bool> DeleteEmployerAsync(int id);
}
