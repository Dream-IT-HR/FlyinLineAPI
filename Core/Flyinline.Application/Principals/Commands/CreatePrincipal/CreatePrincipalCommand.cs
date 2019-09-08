using Flyinline.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Flyinline.Application.Principals.Commands.CreatePrincipal
{
    public class CreatePrincipalCommand: IRequest//, IAuthorizedRequest
    {
        public Guid Id { get; set; }
        public bool SuperAdmin { get; set; }
        public string Username { get; set; }
    }
}
