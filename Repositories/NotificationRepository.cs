using CareerConnect.Data;
using CareerConnect.DTOs;
using CareerConnect.Interfaces;
using CareerConnect.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CareerConnect.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly AppDbContext _context;

        public NotificationRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task SendNotificationAsync(NotificationDTO notificationDto)
        {
            var notification = new Notification
            {
                UserId = notificationDto.UserId,
                Message = notificationDto.Message
            };

            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();
        }

        public async Task<List<NotificationDTO>> GetNotificationsByUserIdAsync(int userId)
        {
            return await _context.Notifications
                .Where(n => n.UserId == userId && !n.IsDeleted)
                .OrderByDescending(n => n.SentAt)
                .Select(n => new NotificationDTO
                {
                    UserId = n.UserId,
                    Message = n.Message
                })
                .ToListAsync();
        }

        public async Task MarkAsReadAsync(int notificationId)
        {
            var notification = await _context.Notifications.FindAsync(notificationId);
            if (notification != null)
            {
                notification.IsRead = true;
                await _context.SaveChangesAsync();
            }
        }
    }
}
