using CareerConnect.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CareerConnect.Interfaces
{
    public interface IResumeRepository
    {
        Task<List<ResumeDTO>> GetAllResumesAsync();
        Task<ResumeDTO> GetResumeByIdAsync(int id);
        Task<List<ResumeDTO>> GetResumesByJobSeekerIdAsync(int jobSeekerId);
        Task<ResumeDTO> CreateResumeAsync(ResumeDTO dto);
        Task<bool> DeleteResumeAsync(int id); // Soft delete
    }
}
