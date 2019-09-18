using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Flyinline.Application.Users.Queries.GetUserDetailByUsername
{
    public class GetUserDetailByUsernameRequest : IRequest<GetUserDetailByUsernameViewModel>
    {
        public string Username { get; set; }
    }
}
