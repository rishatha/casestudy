using CareerConnect.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CareerConnect.Interfaces
{
    public interface INotificationRepository
    {
        Task SendNotificationAsync(NotificationDTO notificationDto);
        Task<List<NotificationDTO>> GetNotificationsByUserIdAsync(int userId);
        Task MarkAsReadAsync(int notificationId);
    }
}
