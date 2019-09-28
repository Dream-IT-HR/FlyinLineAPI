using Flyinline.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Flyinline.WebUI.Security
{
    public class CurrentUserAccessor : ICurrentUserAccessor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserAccessor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetUsername()
        {
            List<Claim> claims = ((ClaimsIdentity)(_httpContextAccessor?.HttpContext?.User?.Identity)).Claims?.ToList();

            var username = claims?.FirstOrDefault(c => c.Type == "Username")?.Value;

            return username;
        }
    }
}
