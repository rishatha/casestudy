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
    public class ApplicationService : IApplicationService
    {
        private readonly AppDbContext _context;

        public ApplicationService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ApplicationDTO>> GetAllApplicationsAsync()
        {
            return await _context.Applications
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
            var application = await _context.Applications.FindAsync(id);
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
                .Where(a => a.JobSeekerId == jobSeekerId)
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
                AppliedAt = applicationDto.AppliedAt
            };

            _context.Applications.Add(application);
            await _context.SaveChangesAsync();

            applicationDto.ApplicationId = application.ApplicationId;
            return applicationDto;
        }

        public async Task<ApplicationDTO> UpdateApplicationStatusAsync(int id, string status)
        {
            var application = await _context.Applications.FindAsync(id);
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
            if (application == null) return false;

            _context.Applications.Remove(application);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
