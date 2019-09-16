using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Flyinline.Application.Principals.Queries.GetPrincipalRoles;
using Flyinline.WebUI.Models;
using Google.Apis.Auth;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Flyinline.WebUI.Services
{
    public class AuthenticateService : IAuthenticateService
    {
        private readonly TokenManagement _tokenManagement;
        private IMediator Mediator;
        

        public AuthenticateService(IOptions<TokenManagement> tokenManagement, IMediator mediator)
        {
            _tokenManagement = tokenManagement.Value;
            Mediator = mediator;
        }
        private async Task<List<string>> GetUserRolesAsync(string username)
        {
            // TODO - get principalid by username - dodati filter express u novi query get principal
            var query = new GetPrincipalRolesRequest() { PrincipalId = new Guid("3FA85F64-5717-4562-B3FC-2C963F66AFA5")  };
            GetPrincipalRolesViewModel t = await Mediator.Send(query);

            return t.Data?.Select(x => x.Role.Name).ToList();
        }

        public bool IsAuthenticated(TokenRequest request, out string token)
        {
            token = string.Empty;
            
            // if (!_userManagementService.IsValidUser(request.Username, request.Password)) return false;

            var claim = new[]
            {
                new Claim(ClaimTypes.Name, request.Username)
            };

            
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenManagement.Secret));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var jwtToken = new JwtSecurityToken(
                _tokenManagement.Issuer,
                _tokenManagement.Audience,
                claim,
                expires: DateTime.Now.AddMinutes(_tokenManagement.AccessExpiration),
                signingCredentials: credentials
            );

            token = new JwtSecurityTokenHandler().WriteToken(jwtToken);

            return true;
        }


        // if (!_userManagementService.IsValidUser(request.Username, request.Password)) return false;

        public async Task<string> GenerateTokenAsync(TokenRequest request)
        {
            string token = string.Empty;
        
            var roles = await GetUserRolesAsync(request.Username);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, request.Username)
            };

            claims.AddRange(roles.Select(x => new Claim(ClaimTypes.Role, x)));
            
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenManagement.Secret));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var jwtToken = new JwtSecurityToken(
                _tokenManagement.Issuer,
                _tokenManagement.Audience,
                claims,
                expires: DateTime.Now.AddMinutes(_tokenManagement.AccessExpiration),
                signingCredentials: credentials
            );

            token = new JwtSecurityTokenHandler().WriteToken(jwtToken);

            return token;

        }

        public async Task<string> AuthenticateGoogle(GoogleJsonWebSignature.Payload payload)
        {
            string token = null;
            if (payload.EmailVerified)
            {
                string username = payload.Email;
                var tr = new TokenRequest()
                {
                    Username = username
                };

                token = await GenerateTokenAsync(tr);
            }

            return token;
        }
    }
}
