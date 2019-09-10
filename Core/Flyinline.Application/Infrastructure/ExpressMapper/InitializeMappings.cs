using ExpressMapper;
using Flyinline.Application.Principals.Commands.CreatePrincipal;
using Flyinline.Application.Users.Commands.Registration;
using Flyinline.Domain.Entities.Common;
using Flyinline.Domain.Entities.Flyinline;
using System;
using System.Collections.Generic;
using System.Text;

namespace Flyinline.Application.Infrastructure.ExpressMapper
{
    public static class ExpressMapperInitializer
    {
        public static void Initialize()
        {
            Mapper.Register<CreatePrincipalCommand, Principal>();
            Mapper.Register<RegisterUserCommand, Principal>();
            Mapper.Register<RegisterUserCommand, UserDetail>();
        }
    }
}
