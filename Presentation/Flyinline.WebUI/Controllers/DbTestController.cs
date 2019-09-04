using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Flyinline.Persistance.Models.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Flyinline.WebUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DbTestController : ControllerBase
    {
        private readonly CommonContext _context;

        public DbTestController(CommonContext context)
        {
            _context = context;
        }

        // GET api/DbTest
        [HttpGet]
        public ActionResult<IEnumerable<PrincipalPermission>> Get()
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

            _context.Add(c);
            _context.Add(p);
            _context.Add(pp);

            _context.SaveChanges();

            var ppView2 = _context.PrincipalPermission.Include("Claim").Select(x => x).Take(1).ToList();

            _context.Remove(c);
            _context.Remove(p);
            _context.Remove(pp);

            _context.SaveChanges();

            return ppView2;
        }

    }
}
