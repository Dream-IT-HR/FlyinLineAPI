using ExpressMapper;
using Flyinline.Application.Interfaces;
using Flyinline.Application.Notifications.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Flyinline.Domain.Entities;
using Flyinline.Application.Shared;

namespace Flyinline.Application.Users.Commands.Registration
{
    public class RegisterUserCommand : IRequest<RegisterUserViewModel>
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName{ get; set; }
        public bool IsBusinessOwner { get; set; }
    }

    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, RegisterUserViewModel>
    {
        private readonly IFlyinlineDbContext _context;
        private readonly IMediator _mediator;

        public RegisterUserCommandHandler(IFlyinlineDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<RegisterUserViewModel> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            Principal p = Mapper.Map<RegisterUserCommand, Principal>(request);
            p.Id = Guid.NewGuid();

            _context.Principal.Add(p);

            string roleName = (request.IsBusinessOwner ? "BusinessOwner" : "Client");

            Guid roleId = _context.Role.Where(r => r.Name == roleName).Select(r => r.Id).FirstOrDefault();

            _context.PrincipalHasRole.Add(new PrincipalHasRole()
            {
                Id = Guid.NewGuid(),
                PrincipalId = p.Id,
                RoleId = roleId
            });

            UserDetail usr = Mapper.Map<RegisterUserCommand, UserDetail>(request);
            usr.Id = p.Id;

            _context.UserDetail.Add(usr);

            await _context.SaveChangesAsync(cancellationToken);

            await _mediator.Publish(new UserRegistered { Data = request }, cancellationToken);

            return new RegisterUserViewModel
            {
                Id = p.Id
            };
        }
    }

}
