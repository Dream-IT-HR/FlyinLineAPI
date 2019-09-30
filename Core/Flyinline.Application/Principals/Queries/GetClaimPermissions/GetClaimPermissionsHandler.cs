using Flyinline.Application.Interfaces;
using Flyinline.Domain.Entities;
using Flyinline.Domain.Views;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Flyinline.Application.Principals.Queries.GetClaimPermissions
{
    public class GetClaimPermissionsHandler : IRequestHandler<GetClaimPermissionsRequest, GetClaimPermissionsViewModel>
    {
        private readonly IFlyinlineDbContext _context;

        public GetClaimPermissionsHandler(IFlyinlineDbContext context)
        {
            _context = context;
        }

        public async Task<GetClaimPermissionsViewModel> Handle(GetClaimPermissionsRequest request, CancellationToken cancellationToken)
        {
            var ret = new GetClaimPermissionsViewModel();

            List<ClaimPermission> ls = await _context.ClaimPermission.Include("Claim").Where(x => x.PrincipalId == request.PrincipalId).ToListAsync(cancellationToken);

            ret.Data.AddRange(ls);

            return ret;
        }
    }
}
