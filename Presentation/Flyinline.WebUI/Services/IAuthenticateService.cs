using Flyinline.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flyinline.WebUI.Services
{
    public interface IAuthenticateService
    {
        bool IsAuthenticated(TokenRequest request, out string token);
        Task<string> GenerateTokenAsync(TokenRequest request);
        Task<string> AuthenticateGoogle(Google.Apis.Auth.GoogleJsonWebSignature.Payload payload);


    }
}
