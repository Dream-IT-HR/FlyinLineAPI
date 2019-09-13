using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Flyinline.Application.Interfaces;
using Flyinline.Domain.Entities;
using Flyinline.Persistance.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Flyinline.WebUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DbTestController : ControllerBase
    {
        private readonly IFlyinlineDbContext _context;
        //private readonly ILogger _logger;

        public DbTestController(IFlyinlineDbContext context)
        {
            _context = context;
        }

        // GET api/DbTest
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PrincipalPermission>>> Get()
        {
            var c = new Claim()
            {
                Id = Guid.NewGuid(),
                Name = "StopLine"
            };

            var p = new Principal()
            {
                Id = Guid.NewGuid(),
                Username = "test@test.hr"
            };

            var pp = new PrincipalPermission()
            {
                Id = Guid.NewGuid(),
                PrincipalId = p.Id,
                ClaimId = c.Id
            };

            _context.Claim.Add(c);
            _context.Principal.Add(p);
            await _context.SaveChangesAsync(new System.Threading.CancellationToken());
            
            
            _context.PrincipalPermission.Add(pp);

            await _context.SaveChangesAsync(new System.Threading.CancellationToken());

            var ppView2 = _context.PrincipalPermission.Include("Claim").Select(x => x).Take(1).ToList();

            _context.Claim.Remove(c);
            _context.Principal.Remove(p);
            _context.PrincipalPermission.Remove(pp);

            await _context.SaveChangesAsync(new System.Threading.CancellationToken());

            // _context.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);

            var r1 = _context.ClaimPermission.Take(1).FirstOrDefault();
            var r2 = _context.ClaimPermission.Include("Principal").Take(1).FirstOrDefault();

            // List<Domain.Views.Common.ClaimPermission> cp = _context.ClaimPermission.ToList();

            return ppView2;
        }

    }
}
