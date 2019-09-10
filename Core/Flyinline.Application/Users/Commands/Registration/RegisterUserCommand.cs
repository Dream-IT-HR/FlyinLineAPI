using Flyinline.Domain.Entities.Flyinline;
using ExpressMapper;
using Flyinline.Application.Interfaces;
using Flyinline.Application.Notifications.Models;
using Flyinline.Domain.Entities.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace Flyinline.Application.Users.Commands.Registration
{
    public class RegisterUserCommand : IRequest
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Nickname { get; set; }
        public bool IsBusinessOwner { get; set; }
    }

    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Unit>
    {
        private readonly ICommonDbContext _commonDbContext;
        private readonly IFlyinlineDbContext _flyinlineDbContext;
        private readonly IMediator _mediator;

        public RegisterUserCommandHandler(ICommonDbContext commonDbContext, IFlyinlineDbContext flyinlineDbContext, IMediator mediator)
        {
            _commonDbContext = commonDbContext;
            _flyinlineDbContext = flyinlineDbContext;
            _mediator = mediator;
        }

        public async Task<Unit> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            Principal p = Mapper.Map<RegisterUserCommand, Principal>(request);
            p.Id = Guid.NewGuid();

            _commonDbContext.Principal.Add(p);

            string roleName = (request.IsBusinessOwner ? "BusinessOwner" : "Client");

            Guid roleId = _commonDbContext.Role.Where(r => r.Name == roleName).Select(r => r.Id).FirstOrDefault();

            _commonDbContext.PrincipalHasRole.Add(new PrincipalHasRole()
            {
                Id = Guid.NewGuid(),
                PrincipalId = p.Id,
                RoleId = roleId
            });

            UserDetail usr = Mapper.Map<RegisterUserCommand, UserDetail>(request);

            _flyinlineDbContext.UserDetail.Add(usr);

            await _commonDbContext.SaveChangesAsync(cancellationToken);
            await _flyinlineDbContext.SaveChangesAsync(cancellationToken);

            await _mediator.Publish(new UserRegistered { Data = request }, cancellationToken);

            return Unit.Value;
        }
    }

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
Fullname: {notification.Data.FullName}
Email: {notification.Data.Email}
{(notification.Data.IsBusinessOwner ? "Business Owner" : "Client")}
                    ",
                }); ;
            }
        }
    }



}
