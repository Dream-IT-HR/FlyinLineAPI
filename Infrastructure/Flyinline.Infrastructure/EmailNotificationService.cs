using Flyinline.Application.Interfaces;
using Flyinline.Application.Notifications.Models;
using System;
using System.Threading.Tasks;

namespace Flyinline.Infrastructure
{
    public class EmailNotificationService : INotificationService
    {
        public async Task SendAsync(Message message)
        {
            await Task.FromResult(Task.CompletedTask);
        }
    }
}
