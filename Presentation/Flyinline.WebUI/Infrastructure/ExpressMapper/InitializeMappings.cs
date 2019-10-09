using ExpressMapper;
using Flyinline.Application.Users.Commands.Registration;
using Flyinline.Domain.Entities;
using Google.Apis.Auth;
using System;
using System.Collections.Generic;
using System.Text;

namespace Flyinline.WebUI.Infrastructure.ExpressMapper
{
    public static class ExpressMapperInitializer
    {
        public static void Initialize()
        {
            Mapper.Register<GoogleJsonWebSignature.Payload, RegisterUserCommand>()
                .Member(dest => dest.FirstName, src => src.GivenName)
                .Member(dest => dest.LastName, src => src.FamilyName)
                .Member(dest => dest.Username, src => src.Email)
                .Member(dest => dest.Email, src => src.Email);
        }
    }
}
