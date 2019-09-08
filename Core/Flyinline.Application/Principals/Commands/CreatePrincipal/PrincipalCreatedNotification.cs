using Flyinline.Application.Interfaces;
using Flyinline.Application.Notifications.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Flyinline.Application.Principals.Commands.CreatePrincipal
{
    public class PrincipalCreated : INotification
    {
        public Guid PrincipalId { get; set; }
        public string Username { get; set; }

        public class PrincipalCreatedHandler : INotificationHandler<PrincipalCreated>
        {
            private readonly INotificationService _notification;

            public PrincipalCreatedHandler(INotificationService notification)
            {
                _notification = notification;
            }

            public async Task Handle(PrincipalCreated notification, CancellationToken cancellationToken)
            {
                await _notification.SendAsync(new Message());
            }

            
        }
    }
}
