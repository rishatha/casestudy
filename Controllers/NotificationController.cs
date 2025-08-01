/*using CareerConnect.DTOs;
using CareerConnect.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CareerConnect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationRepository _notificationRepo;

        public NotificationController(INotificationRepository notificationRepo)
        {
            _notificationRepo = notificationRepo;
        }

        [HttpPost]
        public async Task<IActionResult> SendNotification([FromBody] NotificationDTO notificationDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _notificationRepo.SendNotificationAsync(notificationDto);
            return Ok("Notification sent.");
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserNotifications(int userId)
        {
            var notifications = await _notificationRepo.GetNotificationsByUserIdAsync(userId);
            return Ok(notifications);
        }

        [HttpPut("{id}/read")]
        public async Task<IActionResult> MarkAsRead(int id)
        {
            await _notificationRepo.MarkAsReadAsync(id);
            return NoContent();
        }
    }
}
*/