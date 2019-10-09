using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ExpressMapper;
using Flyinline.Application.Principals.Queries.GetClaimPermissions;
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
            var query = new GetUserDetailByUsernameRequest() { Username = username };

            GetUserDetailByUsernameViewModel t = await Mediator.Send(query);

            return t.Data?.FirstOrDefault();
        }

        private async Task<List<string>> GetPrincipalRolesByPrincipalIdAsync(Guid principalId)
        {
            var query = new GetPrincipalRolesRequest() { PrincipalId = principalId };

            GetPrincipalRolesViewModel t = await Mediator.Send(query);

            return t.Data?.Select(x => x.Role.Name).ToList();
        }

        private async Task<List<string>> GetClaimPermissionsAsync(Guid principalId)
        {
            var query = new GetClaimPermissionsRequest() { PrincipalId = principalId };

            GetClaimPermissionsViewModel t = await Mediator.Send(query);

            return t.Data?.Select(x => x.Claim.Name).ToList();
        }

        public bool IsAuthenticated(TokenRequest request, out string token)
        {
            token = string.Empty;

            // if (!_userManagementService.IsValidUser(request.Username, request.Password)) return false;

            var expires = DateTime.UtcNow.AddDays(_tokenManagement.RefreshExpirationDays);

            var claim = new[]
            {
                new Claim(ClaimTypes.Name, request.Username),
                new Claim("Expires", expires.ToUniversalTime().ToLongDateString())
            };


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenManagement.Secret));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var jwtToken = new JwtSecurityToken(
                _tokenManagement.Issuer,
                _tokenManagement.Audience,
                claim,
                expires: expires,
                signingCredentials: credentials
            );

            token = new JwtSecurityTokenHandler().WriteToken(jwtToken);

            return true;
        }


        public string GenerateRefreshToken(string username)
        {
            string token = string.Empty;

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenManagement.Secret));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.UtcNow.AddDays(_tokenManagement.RefreshExpirationDays);

            var claims = new List<Claim>
            {
                new Claim("Username", username),
                new Claim("Created", DateTime.UtcNow.ToString()),
                new Claim("Expires", expires.ToUniversalTime().ToLongDateString())
            };

            var jwtToken = new JwtSecurityToken(
                _tokenManagement.Issuer,
                _tokenManagement.Audience,
                claims,
                expires: expires,
                signingCredentials: credentials
            );

            token = new JwtSecurityTokenHandler().WriteToken(jwtToken);

            return token;
        }

        public async Task<string> GenerateTokenAsync(string username)
        {
            string token = string.Empty;

            Domain.Entities.UserDetail userDetail = await GetUserDetailByUsernameAsync(username);

            var roles = await GetPrincipalRolesByPrincipalIdAsync(userDetail.Id);
            var claimPermissions = await GetClaimPermissionsAsync(userDetail.Id);

            var expires = DateTime.UtcNow.AddMinutes(_tokenManagement.AccessExpiration);
            var expiresDateTime = DateTime.UtcNow.AddMinutes(_tokenManagement.AccessExpiration);
            var expiresDateTimeOffset = new DateTimeOffset(expiresDateTime);
            var expiresUnixDateTime = expiresDateTimeOffset.ToUnixTimeSeconds();

            var claims = new List<Claim>
            {
                new Claim("Username", username),
                new Claim("FirstName", userDetail.FirstName),
                new Claim("Email", userDetail.Email),
                new Claim("Created", DateTime.UtcNow.ToString()),
                new Claim("Expires", expiresUnixDateTime.ToString())
            };

            claims.Add(new Claim("Claims", string.Join(',', claimPermissions))); 

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenManagement.Secret));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var jwtToken = new JwtSecurityToken(
                _tokenManagement.Issuer,
                _tokenManagement.Audience,
                claims,
                expires: expires,
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
