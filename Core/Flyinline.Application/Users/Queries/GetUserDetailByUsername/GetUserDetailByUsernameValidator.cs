using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Flyinline.Application.Users.Queries.GetUserDetailByUsername
{
    public class GetUserDetailByUsernameValidator : AbstractValidator<GetUserDetailByUsernameRequest>
    {
        public GetUserDetailByUsernameValidator()
        {
            RuleFor(x => x.Username).NotEmpty();
        }
    }
}
