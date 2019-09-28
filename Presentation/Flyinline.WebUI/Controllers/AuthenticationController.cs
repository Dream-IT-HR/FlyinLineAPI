using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Flyinline.Application.Interfaces;
using Flyinline.Persistance.Contexts;
using Flyinline.WebUI.Models;
using Flyinline.WebUI.Services;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Flyinline.WebUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticateService _authenticateService;
        private readonly ICurrentUserAccessor _currentUserAccessor = null;

        public AuthenticationController(IAuthenticateService authenticateService, ICurrentUserAccessor currentUserAccessor)
        {
            _currentUserAccessor = currentUserAccessor;
            _authenticateService = authenticateService;
        }

        [AllowAnonymous]
        [HttpPost, Route("request")]
        public async Task<IActionResult> RequestToken([FromBody] TokenRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string token = await _authenticateService.GenerateTokenAsync(request.Username);
            string refreshToken = _authenticateService.GenerateRefreshToken(request.Username);

            if (!string.IsNullOrEmpty(token))
            {
                return Ok(
                    new
                    {
                        token,
                        refreshToken
                    });
            }

            return BadRequest("Invalid Request");
        }

        // send refresh token in the header
        [Authorize]
        [HttpPost, Route("refresh")]
        public async Task<IActionResult> RefreshToken()
        {
            string username = _currentUserAccessor.GetUsername();

            string token = await _authenticateService.GenerateTokenAsync(username);
            string refreshToken = _authenticateService.GenerateRefreshToken(username);

            if (!string.IsNullOrEmpty(token))
            {
                return Ok(
                    new
                    {
                        token,
                        refreshToken
                    });
            }

            return BadRequest("Invalid Request");
        }

        [AllowAnonymous]
        [HttpPost("google")]
        public async Task<IActionResult> Google([FromBody]GoogleAuthenticateRequest req)
        {
            GoogleJsonWebSignature.Payload payload = await GoogleJsonWebSignature.ValidateAsync(req.TokenId, new GoogleJsonWebSignature.ValidationSettings());

            string token = null;
            string refreshToken = null;

            Guid userId = await _authenticateService.TryRegisterUserFromGoogle(payload);

            if (userId != Guid.Empty)
            {
                token = await _authenticateService.GenerateTokenAsync(payload.Email);
                refreshToken = _authenticateService.GenerateRefreshToken(payload.Email);
            }

            return Ok(
                new
                {
                    token,
                    refreshToken
                });
        }
    }
}

