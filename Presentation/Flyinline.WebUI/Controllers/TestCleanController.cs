using System.Collections.Generic;
using System.Threading.Tasks;
using Flyinline.Application.Principals.Commands.CreatePrincipal;
using Flyinline.Application.Principals.Queries.GetClaimPermissions;
using Flyinline.Application.Principals.Queries.GetPrincipalRoles;
using Flyinline.Domain.Entities.Common;
using Microsoft.AspNetCore.Authorization;
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

        [HttpGet("claim-permissions")]
        [Authorize]
        public async Task<ActionResult<GetClaimPermissionsViewModel>> Get([FromQuery] GetClaimPermissionsRequest query)
        {
            return Ok(await Mediator.Send(query));
        }


        [HttpGet("principal-roles")]
        // [Authorize]
        public async Task<ActionResult<GetPrincipalRolesViewModel>> Get([FromQuery] GetPrincipalRolesRequest query)
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
