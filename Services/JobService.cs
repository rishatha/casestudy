using CareerConnect.Data;
using CareerConnect.DTOs;
using CareerConnect.Interfaces;
using CareerConnect.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CareerConnect.Services
{
    public class JobService : IJobService
    {
        private readonly AppDbContext _context;

        public JobService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<JobDTO>> GetAllJobsAsync()
        {
            return await _context.Jobs
                .Select(j => new JobDTO
                {
                    JobId = j.JobId,
                    EmployerId = j.EmployerId,
                    Title = j.Title,
                    Description = j.Description,
                    Qualifications = j.Qualifications,
                    Location = j.Location,
                    Salary = j.Salary,
                    PostedAt = j.PostedAt,
                    IsActive = j.IsActive
                }).ToListAsync();
        }

        public async Task<JobDTO> GetJobByIdAsync(int id)
        {
            var job = await _context.Jobs.FindAsync(id);
            if (job == null) return null;

            return new JobDTO
            {
                JobId = job.JobId,
                EmployerId = job.EmployerId,
                Title = job.Title,
                Description = job.Description,
                Qualifications = job.Qualifications,
                Location = job.Location,
                Salary = job.Salary,
                PostedAt = job.PostedAt,
                IsActive = job.IsActive
            };
        }

        public async Task<JobDTO> CreateJobAsync(JobDTO jobDto)
        {
            var job = new Job
            {
                EmployerId = jobDto.EmployerId,
                Title = jobDto.Title,
                Description = jobDto.Description,
                Qualifications = jobDto.Qualifications,
                Location = jobDto.Location,
                Salary = jobDto.Salary,
                PostedAt = jobDto.PostedAt,
                IsActive = jobDto.IsActive
            };

            _context.Jobs.Add(job);
            await _context.SaveChangesAsync();

            jobDto.JobId = job.JobId;
            return jobDto;
        }

        public async Task<JobDTO> UpdateJobAsync(int id, JobDTO jobDto)
        {
            var job = await _context.Jobs.FindAsync(id);
            if (job == null) return null;

            job.Title = jobDto.Title;
            job.Description = jobDto.Description;
            job.Qualifications = jobDto.Qualifications;
            job.Location = jobDto.Location;
            job.Salary = jobDto.Salary;
            job.IsActive = jobDto.IsActive;

            await _context.SaveChangesAsync();
            return jobDto;
        }

        public async Task<bool> DeleteJobAsync(int id)
        {
            var job = await _context.Jobs.FindAsync(id);
            if (job == null) return false;

            _context.Jobs.Remove(job);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
