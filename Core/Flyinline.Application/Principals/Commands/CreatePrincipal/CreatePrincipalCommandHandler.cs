using ExpressMapper;
using Flyinline.Application.Interfaces;
using Flyinline.Domain.Entities.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Flyinline.Application.Principals.Commands.CreatePrincipal
{

    public class CreatePrincipalCommandHandler : IRequestHandler<CreatePrincipalCommand, Unit>
    {
        private readonly ICommonDbContext _context;
        private readonly IMediator _mediator;

        public CreatePrincipalCommandHandler(ICommonDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<Unit> Handle(CreatePrincipalCommand request, CancellationToken cancellationToken)
        {
            Principal p = Mapper.Map<CreatePrincipalCommand, Principal>(request);

            _context.Principal.Add(p);

            await _context.SaveChangesAsync(cancellationToken);

            await _mediator.Publish(new PrincipalCreated { PrincipalId = p.Id, Username = p.Username }, cancellationToken);

            return Unit.Value;
        }
    }
}
