using CareerConnect.Data;
using CareerConnect.DTOs;
using CareerConnect.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CareerConnect.Repositories
{
    public class EmployerRepository : IEmployerRepository
    {
        private readonly AppDbContext _context;

        public EmployerRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<EmployerDTO> CreateEmployerAsync(EmployerDTO dto)
        {
            var employer = new Employer
            {
                UserId = dto.UserId,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                PhoneNumber = dto.PhoneNumber,
                CompanyName = dto.CompanyName,
                Website = dto.Website
            };

            _context.Employers.Add(employer);
            await _context.SaveChangesAsync();

            dto.EmployerId = employer.EmployerId;
            return dto;
        }

        public async Task<EmployerDTO> GetEmployerByIdAsync(int id)
        {
            var emp = await _context.Employers
                .Where(e => e.EmployerId == id && e.IsActive)
                .FirstOrDefaultAsync();

            if (emp == null) return null;

            return new EmployerDTO
            {
                EmployerId = emp.EmployerId,
                UserId = emp.UserId,
                FirstName = emp.FirstName,
                LastName = emp.LastName,
                PhoneNumber = emp.PhoneNumber,
                CompanyName = emp.CompanyName,
                Website = emp.Website
            };
        }

        public async Task<IEnumerable<EmployerDTO>> GetAllEmployersAsync()
        {
            return await _context.Employers
                .Where(e => e.IsActive)
                .Select(emp => new EmployerDTO
                {
                    EmployerId = emp.EmployerId,
                    UserId = emp.UserId,
                    FirstName = emp.FirstName,
                    LastName = emp.LastName,
                    PhoneNumber = emp.PhoneNumber,
                    CompanyName = emp.CompanyName,
                    Website = emp.Website
                })
                .ToListAsync();
        }

        public async Task<EmployerDTO> UpdateEmployerAsync(int id, EmployerDTO dto)
        {
            var emp = await _context.Employers.FindAsync(id);
            if (emp == null || !emp.IsActive) return null;

            emp.FirstName = dto.FirstName;
            emp.LastName = dto.LastName;
            emp.PhoneNumber = dto.PhoneNumber;
            emp.CompanyName = dto.CompanyName;
            emp.Website = dto.Website;

            await _context.SaveChangesAsync();
            return dto;
        }

        public async Task<bool> DeleteEmployerAsync(int id)
        {
            var emp = await _context.Employers.FindAsync(id);
            if (emp == null || !emp.IsActive) return false;

            emp.IsActive = false;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
