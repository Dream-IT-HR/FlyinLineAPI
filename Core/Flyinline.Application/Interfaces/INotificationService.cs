using Flyinline.Application.Notifications.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Flyinline.Application.Interfaces
{
    public interface INotificationService
    {
        Task SendAsync(Message message);
    }
}
