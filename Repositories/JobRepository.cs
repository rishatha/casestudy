using CareerConnect.Data;
using CareerConnect.DTOs;
using CareerConnect.Exceptions;
using CareerConnect.Interfaces;
using CareerConnect.Models;
using Microsoft.EntityFrameworkCore;

namespace CareerConnect.Repositories
{
    public class JobRepository : IJobRepository
    {
        private readonly AppDbContext _context;

        public JobRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Job>> GetAllJobsAsync()
        {
            var jobs = await _context.Jobs.Where(j => j.IsActive).ToListAsync();

            if (jobs == null || jobs.Count == 0)
                throw new NotFoundException("No active jobs found.");

            return jobs;
        }

        public async Task<Job> GetJobByIdAsync(int id)
        {
            var job = await _context.Jobs.FirstOrDefaultAsync(j => j.JobId == id && j.IsActive);

            if (job == null)
                throw new NotFoundException($"Job with ID {id} not found.");

            return job;
        }

        public async Task<Job> CreateJobAsync(JobDTO jobDto)
        {
            if (jobDto == null)
                throw new ValidationException("Job data must be provided.");

            if (jobDto.Salary <= 0)
                throw new ValidationException("Salary must be greater than zero.");

            var job = new Job
            {
                EmployerId = jobDto.EmployerId,
                Title = jobDto.Title,
                Description = jobDto.Description,
                Qualifications = jobDto.Qualifications,
                Location = jobDto.Location,
                Salary = jobDto.Salary,
                CompanyName = jobDto.CompanyName,
                PostedDate = jobDto.PostedDate,
                IsActive = true
            };

            _context.Jobs.Add(job);
            await _context.SaveChangesAsync();

            return job;
        }

        public async Task<Job> UpdateJobAsync(int id, JobDTO jobDto)
        {
            if (jobDto == null)
                throw new ValidationException("Job data must be provided.");

            var job = await _context.Jobs.FirstOrDefaultAsync(j => j.JobId == id && j.IsActive);

            if (job == null)
                throw new NotFoundException($"Job with ID {id} not found.");

            job.Title = jobDto.Title;
            job.Description = jobDto.Description;
            job.Qualifications = jobDto.Qualifications;
            job.Location = jobDto.Location;
            job.Salary = jobDto.Salary;
            job.CompanyName = jobDto.CompanyName;
            job.PostedDate = jobDto.PostedDate;

            await _context.SaveChangesAsync();

            return job;
        }

        public async Task<bool> DeleteJobAsync(int id)
        {
            var job = await _context.Jobs.FirstOrDefaultAsync(j => j.JobId == id && j.IsActive);

            if (job == null)
                throw new NotFoundException($"Job with ID {id} not found.");

            job.IsActive = false;
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
