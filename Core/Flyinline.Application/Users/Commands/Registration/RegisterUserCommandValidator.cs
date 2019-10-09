using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Flyinline.Application.Users.Commands.Registration
{
    public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator()
        {
            RuleFor(x => x.Username).NotEmpty();
            RuleFor(x => x.Email).EmailAddress().NotEmpty();
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.LastName).NotEmpty();
            
        }
    }
}
