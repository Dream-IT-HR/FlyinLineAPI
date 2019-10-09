using Flyinline.Application.Interfaces;
using Flyinline.Application.Notifications.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Flyinline.Application.Users.Commands.Registration
{
    public class UserRegistered : INotification
    {
        public RegisterUserCommand Data { get; set; }

        public class UserRegisteredHandler : INotificationHandler<UserRegistered>
        {
            private readonly INotificationService _notification;

            public UserRegisteredHandler(INotificationService notification)
            {
                _notification = notification;
            }

            public async Task Handle(UserRegistered notification, CancellationToken cancellationToken)
            {
                await _notification.SendAsync(new Message()
                {
                    Subject = $"New Registration {(notification.Data.IsBusinessOwner ? "Business Owner" : "Client")}",
                    Body =
$@"New Registration {(notification.Data.IsBusinessOwner ? "Business Owner" : "Client")}
Username: {notification.Data.Username}
Fullname: {notification.Data.FirstName} {notification.Data.LastName}
Email: {notification.Data.Email}
{(notification.Data.IsBusinessOwner ? "Business Owner" : "Client")}
                    ",
                }); ;
            }
        }
    }

}
