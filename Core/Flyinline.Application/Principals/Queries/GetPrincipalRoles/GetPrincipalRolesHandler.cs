using Flyinline.Application.Interfaces;
using Flyinline.Domain.Entities.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Flyinline.Application.Principals.Queries.GetPrincipalRoles
{
    public class GetPrincipalRolesHandler : IRequestHandler<GetPrincipalRolesRequest, GetPrincipalRolesViewModel>
    {
        private readonly ICommonDbContext _context;

        public GetPrincipalRolesHandler(ICommonDbContext context)
        {
            _context = context;
        }

        public async Task<GetPrincipalRolesViewModel> Handle(GetPrincipalRolesRequest request, CancellationToken cancellationToken)
        {
            var ret = new GetPrincipalRolesViewModel();

            List<PrincipalHasRole> ls = await _context.PrincipalHasRole.Include("Role").Where(x => x.PrincipalId == request.PrincipalId).ToListAsync(cancellationToken);

            ret.Data.AddRange(ls);

            return ret;
        }
    }
}
