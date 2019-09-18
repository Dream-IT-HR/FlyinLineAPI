using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ExpressMapper;
using Flyinline.Application.Principals.Queries.GetPrincipalRoles;
using Flyinline.Application.Users.Commands.Registration;
using Flyinline.Application.Users.Queries.GetUserDetailByUsername;
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

        private async Task<Domain.Entities.UserDetail> GetUserDetailByUsernameAsync(string username)
        {
            var query = new GetUserDetailByUsernameRequest() { Username = username};

            GetUserDetailByUsernameViewModel t = await Mediator.Send(query);

            return t.Data?.FirstOrDefault();
        }

        private async Task<List<string>> GetPrincipalRolesByPrincipalIdAsync(Guid principalId)
        {
            var query = new GetPrincipalRolesRequest() { PrincipalId = principalId };

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

        public async Task<string> GenerateTokenAsync(string username)
        {
            string token = string.Empty;

            Domain.Entities.UserDetail userDetail = await GetUserDetailByUsernameAsync(username);

            var roles = await GetPrincipalRolesByPrincipalIdAsync(userDetail.Id);

            var claims = new List<Claim>
            {
                new Claim("Username", username),
                new Claim("Nickname", userDetail.Nickname),
                new Claim("Email", userDetail.Email)
            };
            
            claims.Add(new Claim("Userroles", string.Join(',', roles)));

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

        public async Task<Guid> TryRegisterUserFromGoogle(GoogleJsonWebSignature.Payload payload)
        {
            Guid userId = Guid.Empty;

            if (payload.EmailVerified)
            {
                var user = await GetUserDetailByUsernameAsync(payload.Email);

                if (user == null)
                {
                    RegisterUserCommand registerUserCommand = Mapper.Map<GoogleJsonWebSignature.Payload, RegisterUserCommand>(payload);

                    RegisterUserViewModel ruvm = await Mediator.Send(registerUserCommand);

                    userId = ruvm.Id;
                }
                else
                {
                    userId = user.Id;
                }
            }

            return userId;
        }
    }
}
