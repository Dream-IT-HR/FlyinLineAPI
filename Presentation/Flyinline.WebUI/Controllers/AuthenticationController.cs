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
        public AuthenticationController(IAuthenticateService authenticateService)
        {
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

            string token = await _authenticateService.GenerateTokenAsync(request);

            //if (_authenticateService.IsAuthenticated(request, out token))
            if (!string.IsNullOrEmpty(token))
            {
                return Ok(token);
            }

            return BadRequest("Invalid Request");
        }

        [AllowAnonymous]
        [HttpPost("google")]
        public async Task<IActionResult> Google([FromBody]GoogleAuthenticateRequest req)
        {
            GoogleJsonWebSignature.Payload payload = GoogleJsonWebSignature.ValidateAsync(req.TokenId, new GoogleJsonWebSignature.ValidationSettings()).Result;

            await _authenticateService.GenerateTokenAsync(
                new TokenRequest()
                {
                    Username = payload.Email
                });

            return null;
        }
    }
}

