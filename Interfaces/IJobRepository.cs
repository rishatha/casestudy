using CareerConnect.DTOs;
using CareerConnect.Models;

namespace CareerConnect.Interfaces
{
    public interface IJobRepository
    {
        Task<IEnumerable<Job>> GetAllJobsAsync();
        Task<Job?> GetJobByIdAsync(int id);
        Task<Job> CreateJobAsync(JobDTO jobDto);
        Task<Job?> UpdateJobAsync(int id, JobDTO jobDto);
        Task<bool> DeleteJobAsync(int id); // soft delete
    }
}
