using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Flyinline.Application.Principals.Commands.CreatePrincipal
{
    public class CreatePrincipalCommandValidator : AbstractValidator<CreatePrincipalCommand>
    {
        public CreatePrincipalCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Username).NotEmpty();
            RuleFor(x => x.Username).EmailAddress();
        }
    }
}
