using CareerConnect.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CareerConnect.Interfaces
{
    public interface IJobService
    {
        Task<IEnumerable<JobDTO>> GetAllJobsAsync();
        Task<JobDTO> GetJobByIdAsync(int id);
        Task<JobDTO> CreateJobAsync(JobDTO jobDto);
        Task<JobDTO> UpdateJobAsync(int id, JobDTO jobDto);
        Task<bool> DeleteJobAsync(int id);
    }
}
