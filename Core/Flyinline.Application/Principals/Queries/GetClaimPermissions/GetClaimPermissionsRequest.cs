﻿using Flyinline.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Flyinline.Application.Principals.Queries.GetClaimPermissions
{
    public class GetClaimPermissionsRequest: IRequest<GetClaimPermissionsViewModel>, IAuthorizedRequest
    {
        public Guid PrincipalId { get; set; }
    }
}
