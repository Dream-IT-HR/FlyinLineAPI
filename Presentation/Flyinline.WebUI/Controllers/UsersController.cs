using System.Collections.Generic;
using System.Threading.Tasks;
using Flyinline.Application.Users.Commands.Registration;
using Flyinline.Application.Users.Queries.GetUserDetailByUsername;
using Microsoft.AspNetCore.Authorization;
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

        [HttpGet("{username}")]
        [Authorize]
        public async Task<ActionResult<GetUserDetailByUsernameViewModel>> Get(string username)
        {
            var query = new GetUserDetailByUsernameRequest() { Username = username };

            GetUserDetailByUsernameViewModel res = await Mediator.Send(query);

            return Ok(res);
        }
    }
}
