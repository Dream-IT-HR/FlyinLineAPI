using Flyinline.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Flyinline.Application.Principals.Queries.GetPrincipalRoles
{
    public class GetPrincipalRolesRequest: IRequest<GetPrincipalRolesViewModel>
    {
        public Guid PrincipalId { get; set; }
    }
}
