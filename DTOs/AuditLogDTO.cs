using System;

namespace CareerConnect.DTOs
{
    public class AuditLogDTO
    {
        public int LogId { get; set; }
        public int UserId { get; set; }
        public string Action { get; set; }
        public string Description { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
