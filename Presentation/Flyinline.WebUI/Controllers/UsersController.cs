using System.Collections.Generic;
using System.Threading.Tasks;
using Flyinline.Application.Users.Commands.Registration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Flyinline.WebUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : BaseController
    {
        
        public UsersController()
        {
            
        }

        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Register([FromBody]RegisterUserCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

    }
}
