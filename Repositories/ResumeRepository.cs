using CareerConnect.Data;
using CareerConnect.DTOs;
using CareerConnect.Exceptions;
using CareerConnect.Interfaces;
using CareerConnect.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CareerConnect.Repositories
{
    public class ResumeRepository : IResumeRepository
    {
        private readonly AppDbContext _context;

        public ResumeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ResumeDTO>> GetAllResumesAsync()
        {
            return await _context.Resumes
                .Where(r => !r.IsDeleted)
                .Select(r => new ResumeDTO
                {
                    ResumeId = r.ResumeId,
                    JobSeekerId = r.JobSeekerId,
                    ResumePath = r.ResumePath,
                    UploadedAt = r.UploadedAt
                }).ToListAsync();
        }

        public async Task<ResumeDTO> GetResumeByIdAsync(int id)
        {
            var resume = await _context.Resumes
                .FirstOrDefaultAsync(r => r.ResumeId == id && !r.IsDeleted);

            if (resume == null)
                throw new NotFoundException($"Resume with ID {id} not found.");

            return new ResumeDTO
            {
                ResumeId = resume.ResumeId,
                JobSeekerId = resume.JobSeekerId,
                ResumePath = resume.ResumePath,
                UploadedAt = resume.UploadedAt
            };
        }

        public async Task<List<ResumeDTO>> GetResumesByJobSeekerIdAsync(int jobSeekerId)
        {
            return await _context.Resumes
                .Where(r => r.JobSeekerId == jobSeekerId && !r.IsDeleted)
                .Select(r => new ResumeDTO
                {
                    ResumeId = r.ResumeId,
                    JobSeekerId = r.JobSeekerId,
                    ResumePath = r.ResumePath,
                    UploadedAt = r.UploadedAt
                }).ToListAsync();
        }

        public async Task<ResumeDTO> CreateResumeAsync(ResumeDTO dto)
        {
            if (dto.JobSeekerId <= 0 || string.IsNullOrEmpty(dto.ResumePath))
                throw new ValidationException("Invalid job seeker ID or resume path.");

            var resume = new Resume
            {
                JobSeekerId = dto.JobSeekerId,
                ResumePath = dto.ResumePath,
                UploadedAt = dto.UploadedAt
            };

            _context.Resumes.Add(resume);
            await _context.SaveChangesAsync();

            dto.ResumeId = resume.ResumeId;
            return dto;
        }

        public async Task<bool> DeleteResumeAsync(int id)
        {
            var resume = await _context.Resumes.FindAsync(id);

            if (resume == null)
                throw new NotFoundException($"Resume with ID {id} not found.");

            if (resume.IsDeleted)
                throw new BadRequestException("Resume already deleted.");

            resume.IsDeleted = true;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
