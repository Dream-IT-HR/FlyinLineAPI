using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Flyinline.Application.Principals.Queries.GetPrincipalRoles
{
    public class GetPrincipalRolesValidator : AbstractValidator<GetPrincipalRolesRequest>
    {
        public GetPrincipalRolesValidator()
        {
            RuleFor(x => x.PrincipalId).NotEmpty();
        }
    }
}
