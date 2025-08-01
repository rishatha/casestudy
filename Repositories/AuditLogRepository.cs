/*using CareerConnect.Data;
using CareerConnect.DTOs;
using CareerConnect.Interfaces;
using CareerConnect.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CareerConnect.Repositories
{
    public class AuditLogRepository : IAuditLogRepository
    {
        private readonly AppDbContext _context;

        public AuditLogRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddLogAsync(int userId, string action, string description)
        {
            var log = new AuditLog
            {
                UserId = userId,
                Action = action,
                Description = description,
                Timestamp = DateTime.UtcNow
            };

            _context.AuditLogs.Add(log);
            await _context.SaveChangesAsync();
        }

        public async Task<List<AuditLogDTO>> GetAllLogsAsync()
        {
            return await _context.AuditLogs
                .Select(log => new AuditLogDTO
                {
                    LogId = log.LogId,
                    UserId = log.UserId,
                    Action = log.Action,
                    Description = log.Description,
                    Timestamp = log.Timestamp
                })
                .ToListAsync();
        }

        public async Task<List<AuditLogDTO>> GetLogsByUserIdAsync(int userId)
        {
            return await _context.AuditLogs
                .Where(log => log.UserId == userId)
                .Select(log => new AuditLogDTO
                {
                    LogId = log.LogId,
                    UserId = log.UserId,
                    Action = log.Action,
                    Description = log.Description,
                    Timestamp = log.Timestamp
                })
                .ToListAsync();
        }
    }
}
*/
