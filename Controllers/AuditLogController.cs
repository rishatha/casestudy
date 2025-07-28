using CareerConnect.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CareerConnect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuditLogController : ControllerBase
    {
        private readonly IAuditLogRepository _auditLogRepo;

        public AuditLogController(IAuditLogRepository auditLogRepo)
        {
            _auditLogRepo = auditLogRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var logs = await _auditLogRepo.GetAllLogsAsync();
            return Ok(logs);
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetByUserId(int userId)
        {
            var logs = await _auditLogRepo.GetLogsByUserIdAsync(userId);
            return Ok(logs);
        }

        [HttpPost]
        public async Task<IActionResult> AddLog(int userId, string action, string description)
        {
            await _auditLogRepo.AddLogAsync(userId, action, description);
            return Ok("Log added.");
        }
    }
}
