using CareerConnect.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CareerConnect.Interfaces
{
    public interface IJobSeekerService
    {
        Task<IEnumerable<JobSeekerDTO>> GetAllAsync();
        Task<JobSeekerDTO> GetByIdAsync(int id);
        Task<JobSeekerDTO> CreateAsync(JobSeekerDTO dto);
        Task<JobSeekerDTO> UpdateAsync(int id, JobSeekerDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
