using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Flyinline.Application.Interfaces;
using Flyinline.Application.Users.Queries.GetUserDetailByUsername;
using Flyinline.Domain.Entities;
using Flyinline.Persistance.Contexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Flyinline.WebUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DbTestController : BaseController
    {
        private readonly IFlyinlineDbContext _context;
        public DbTestController(IFlyinlineDbContext context)
        {
            _context = context;
        }

        
    }
}
