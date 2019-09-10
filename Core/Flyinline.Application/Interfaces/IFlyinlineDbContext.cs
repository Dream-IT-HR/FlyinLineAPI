using Flyinline.Domain.Entities.Flyinline;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Flyinline.Application.Interfaces
{
    public interface IFlyinlineDbContext
    {
        DbSet<UserDetail> UserDetail{ get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
