using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Flyinline.Application.Principals.Queries.GetClaimPermissions
{
    public class GetClaimPermissionsRequest: IRequest<GetClaimPermissionsViewModel>
    {
        public Guid PrincipalId { get; set; }
    }
}
