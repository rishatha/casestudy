using CareerConnect.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CareerConnect.Interfaces
{
    public interface IApplicationService
    {
        Task<IEnumerable<ApplicationDTO>> GetAllApplicationsAsync();
        Task<ApplicationDTO> GetApplicationByIdAsync(int id);
        Task<IEnumerable<ApplicationDTO>> GetApplicationsByJobSeekerIdAsync(int jobSeekerId);
        Task<ApplicationDTO> CreateApplicationAsync(ApplicationDTO applicationDto);
        Task<ApplicationDTO> UpdateApplicationStatusAsync(int id, string status);
        Task<bool> DeleteApplicationAsync(int id);
    }
}
