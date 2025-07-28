using CareerConnect.Data;
using CareerConnect.DTOs;
using CareerConnect.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CareerConnect.Repositories
{
    public class JobSeekerRepository : IJobSeekerRepository
    {
        private readonly AppDbContext _context;

        public JobSeekerRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<JobSeekerDTO>> GetAllAsync()
        {
            return await _context.JobSeekers
                .Where(js => js.IsActive)
                .Select(js => new JobSeekerDTO
                {
                    JobSeekerID = js.JobSeekerId,
                    UserId = js.UserId,
                    FirstName = js.FirstName,
                    LastName = js.LastName,
                    Phone = js.Phone,
                    Qualification = js.Qualification,
                    Skills = js.Skills
                }).ToListAsync();
        }

        public async Task<JobSeekerDTO> GetByIdAsync(int id)
        {
            var js = await _context.JobSeekers
                .FirstOrDefaultAsync(j => j.JobSeekerId == id && j.IsActive);

            if (js == null) return null;

            return new JobSeekerDTO
            {
                JobSeekerID = js.JobSeekerId,
                UserId = js.UserId,
                FirstName = js.FirstName,
                LastName = js.LastName,
                Phone = js.Phone,
                Qualification = js.Qualification,
                Skills = js.Skills
            };
        }

        public async Task<JobSeekerDTO> CreateAsync(JobSeekerDTO dto)
        {
            var entity = new JobSeeker
            {
                UserId = dto.UserId,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Phone = dto.Phone,
                Qualification = dto.Qualification,
                Skills = dto.Skills
            };

            _context.JobSeekers.Add(entity);
            await _context.SaveChangesAsync();

            dto.JobSeekerID = entity.JobSeekerId;
            return dto;
        }

        public async Task<JobSeekerDTO> UpdateAsync(int id, JobSeekerDTO dto)
        {
            var entity = await _context.JobSeekers.FindAsync(id);
            if (entity == null || !entity.IsActive) return null;

            entity.FirstName = dto.FirstName;
            entity.LastName = dto.LastName;
            entity.Phone = dto.Phone;
            entity.Qualification = dto.Qualification;
            entity.Skills = dto.Skills;

            await _context.SaveChangesAsync();
            return dto;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.JobSeekers.FindAsync(id);
            if (entity == null || !entity.IsActive) return false;

            entity.IsActive = false;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
