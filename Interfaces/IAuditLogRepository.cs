using CareerConnect.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CareerConnect.Interfaces
{
    public interface IAuditLogRepository
    {
        Task AddLogAsync(int userId, string action, string description);
        Task<List<AuditLogDTO>> GetAllLogsAsync();
        Task<List<AuditLogDTO>> GetLogsByUserIdAsync(int userId);
    }
}
