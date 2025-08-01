/*using CareerConnect.Data;
using CareerConnect.DTOs;
using CareerConnect.Interfaces;
using CareerConnect.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CareerConnect.Repositories
{
    public class ApplicationRepository : IApplicationRepository
    {
        private readonly AppDbContext _context;

        public ApplicationRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ApplicationDTO>> GetAllApplicationsAsync()
        {
            return await _context.Applications
                .Where(a => a.IsActive)
                .Select(a => new ApplicationDTO
                {
                    ApplicationId = a.ApplicationId,
                    JobId = a.JobId,
                    JobSeekerId = a.JobSeekerId,
                    Status = a.Status,
                    AppliedAt = a.AppliedAt
                }).ToListAsync();
        }

        public async Task<ApplicationDTO> GetApplicationByIdAsync(int id)
        {
            var application = await _context.Applications
                .Where(a => a.ApplicationId == id && a.IsActive)
                .FirstOrDefaultAsync();

            if (application == null) return null;

            return new ApplicationDTO
            {
                ApplicationId = application.ApplicationId,
                JobId = application.JobId,
                JobSeekerId = application.JobSeekerId,
                Status = application.Status,
                AppliedAt = application.AppliedAt
            };
        }

        public async Task<IEnumerable<ApplicationDTO>> GetApplicationsByJobSeekerIdAsync(int jobSeekerId)
        {
            return await _context.Applications
                .Where(a => a.JobSeekerId == jobSeekerId && a.IsActive)
                .Select(a => new ApplicationDTO
                {
                    ApplicationId = a.ApplicationId,
                    JobId = a.JobId,
                    JobSeekerId = a.JobSeekerId,
                    Status = a.Status,
                    AppliedAt = a.AppliedAt
                }).ToListAsync();
        }

        public async Task<ApplicationDTO> CreateApplicationAsync(ApplicationDTO applicationDto)
        {
            var application = new Application
            {
                JobId = applicationDto.JobId,
                JobSeekerId = applicationDto.JobSeekerId,
                Status = applicationDto.Status,
                AppliedAt = applicationDto.AppliedAt,
                IsActive = true
            };

            _context.Applications.Add(application);
            await _context.SaveChangesAsync();

            applicationDto.ApplicationId = application.ApplicationId;
            return applicationDto;
        }

        public async Task<ApplicationDTO> UpdateApplicationStatusAsync(int id, string status)
        {
            var application = await _context.Applications
                .Where(a => a.ApplicationId == id && a.IsActive)
                .FirstOrDefaultAsync();

            if (application == null) return null;

            application.Status = status;
            await _context.SaveChangesAsync();

            return new ApplicationDTO
            {
                ApplicationId = application.ApplicationId,
                JobId = application.JobId,
                JobSeekerId = application.JobSeekerId,
                Status = application.Status,
                AppliedAt = application.AppliedAt
            };
        }

        public async Task<bool> DeleteApplicationAsync(int id)
        {
            var application = await _context.Applications.FindAsync(id);
            if (application == null || !application.IsActive) return false;

            application.IsActive = false; // Soft delete
            await _context.SaveChangesAsync();
            return true;
        }
      

    }
}
*/
using CareerConnect.Data;
using CareerConnect.DTOs;
using CareerConnect.Exceptions;
using CareerConnect.Interfaces;
using CareerConnect.Models;
using Microsoft.EntityFrameworkCore;

namespace CareerConnect.Repositories
{
    public class ApplicationRepository : IApplicationRepository
    {
        private readonly AppDbContext _context;
        private readonly IEmailService _emailService;

        public ApplicationRepository(AppDbContext context, IEmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        public async Task<IEnumerable<ApplicationDTO>> GetAllApplicationsAsync()
        {
            return await _context.Applications
                .Where(a => a.IsActive)
                .Select(a => new ApplicationDTO
                {
                    ApplicationId = a.ApplicationId,
                    JobId = a.JobId,
                    JobSeekerId = a.JobSeekerId,
                    Status = a.Status,
                    AppliedAt = a.AppliedAt
                }).ToListAsync();
        }

        public async Task<ApplicationDTO> GetApplicationByIdAsync(int id)
        {
            var application = await _context.Applications
                .Where(a => a.ApplicationId == id && a.IsActive)
                .FirstOrDefaultAsync();

            if (application == null)
                throw new NotFoundException("Application not found.");

            return new ApplicationDTO
            {
                ApplicationId = application.ApplicationId,
                JobId = application.JobId,
                JobSeekerId = application.JobSeekerId,
                Status = application.Status,
                AppliedAt = application.AppliedAt
            };
        }

        public async Task<IEnumerable<ApplicationDTO>> GetApplicationsByJobSeekerIdAsync(int jobSeekerId)
        {
            return await _context.Applications
                .Where(a => a.JobSeekerId == jobSeekerId && a.IsActive)
                .Select(a => new ApplicationDTO
                {
                    ApplicationId = a.ApplicationId,
                    JobId = a.JobId,
                    JobSeekerId = a.JobSeekerId,
                    Status = a.Status,
                    AppliedAt = a.AppliedAt
                }).ToListAsync();
        }

        public async Task<ApplicationDTO> CreateApplicationAsync(ApplicationDTO applicationDto)
        {
            if (applicationDto.JobId <= 0 || applicationDto.JobSeekerId <= 0)
                throw new ValidationException("Job ID and Job Seeker ID must be valid.");

            var application = new Application
            {
                JobId = applicationDto.JobId,
                JobSeekerId = applicationDto.JobSeekerId,
                Status = applicationDto.Status,
                AppliedAt = applicationDto.AppliedAt,
                IsActive = true
            };

            _context.Applications.Add(application);
            await _context.SaveChangesAsync();

            // Fetch user emails
            var job = await _context.Jobs.Include(j => j.Employer).FirstOrDefaultAsync(j => j.JobId == application.JobId);
            if (job == null)
                throw new NotFoundException("Job not found for email notification.");

            var employerUser = await _context.Users.FirstOrDefaultAsync(u => u.UserId == job.Employer.UserId);
            var jobSeeker = await _context.JobSeekers.FirstOrDefaultAsync(js => js.JobSeekerId == application.JobSeekerId);
            var jobSeekerUser = await _context.Users.FirstOrDefaultAsync(u => u.UserId == jobSeeker.UserId);

            if (employerUser != null)
            {
                await _emailService.SendEmailAsync(
                    employerUser.Email,
                    "New Job Application Received",
                    $"A new application has been submitted for your job: {job.Title}."
                );
            }

            if (jobSeekerUser != null)
            {
                await _emailService.SendEmailAsync(
                    jobSeekerUser.Email,
                    "Application Submitted Successfully",
                    $"You have successfully applied for the job: {job.Title}."
                );
            }

            applicationDto.ApplicationId = application.ApplicationId;
            return applicationDto;
        }

        public async Task<ApplicationDTO> UpdateApplicationStatusAsync(int id, string status)
        {
            if (string.IsNullOrWhiteSpace(status))
                throw new ValidationException("Application status must not be empty.");

            var application = await _context.Applications
                .Include(a => a.Job)
                    .ThenInclude(j => j.Employer)
                .FirstOrDefaultAsync(a => a.ApplicationId == id && a.IsActive);

            if (application == null)
                throw new NotFoundException("Application not found.");

            application.Status = status;
            await _context.SaveChangesAsync();

            var jobSeeker = await _context.JobSeekers.FirstOrDefaultAsync(js => js.JobSeekerId == application.JobSeekerId);
            var jobSeekerUser = await _context.Users.FirstOrDefaultAsync(u => u.UserId == jobSeeker.UserId);

            if (jobSeekerUser != null)
            {
                await _emailService.SendEmailAsync(
                    jobSeekerUser.Email,
                    "Application Status Updated",
                    $"The status of your application for job '{application.Job.Title}' has been updated to: {status}."
                );
            }

            return new ApplicationDTO
            {
                ApplicationId = application.ApplicationId,
                JobId = application.JobId,
                JobSeekerId = application.JobSeekerId,
                Status = application.Status,
                AppliedAt = application.AppliedAt
            };
        }

        public async Task<bool> DeleteApplicationAsync(int id)
        {
            var application = await _context.Applications.FindAsync(id);
            if (application == null || !application.IsActive)
                throw new NotFoundException("Application not found or already deleted.");

            application.IsActive = false;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}


