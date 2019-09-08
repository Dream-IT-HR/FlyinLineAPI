using System.Collections.Generic;
using System.Threading.Tasks;
using Flyinline.Application.Principals.Commands.CreatePrincipal;
using Flyinline.Application.Principals.Queries.GetClaimPermissions;
using Flyinline.Domain.Entities.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Flyinline.WebUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestCleanController : BaseController
    {
        
        public TestCleanController()
        {
            
        }

        [HttpGet("principal-permissions")]
        public async Task<ActionResult<GetClaimPermissionsViewModel>> Get([FromQuery] GetClaimPermissionsRequest query)
        {
            return Ok(await Mediator.Send(query));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Create([FromBody]CreatePrincipalCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

    }
}
