using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Flyinline.Application.Principals.Queries.GetClaimPermissions
{
    public class GetClaimPermissionsValidator : AbstractValidator<GetClaimPermissionsRequest>
    {
        public GetClaimPermissionsValidator()
        {
            RuleFor(x => x.PrincipalId).NotEmpty();
        }
    }
}
