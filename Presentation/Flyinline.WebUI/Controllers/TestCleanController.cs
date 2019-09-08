using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Flyinline.Application.Interfaces;
using Flyinline.Application.Principals.Commands.CreatePrincipal;
using Flyinline.Domain.Entities.Common;
using Flyinline.Persistance.Contexts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Flyinline.WebUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestCleanController : BaseController
    {
        
        public TestCleanController()
        {
            
        }

        //// GET api/TestClean
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<PrincipalPermission>>> Get()
        //{

        //}

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
