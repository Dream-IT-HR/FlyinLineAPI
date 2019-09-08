using Flyinline.Domain.Entities.Common;
using Flyinline.Domain.Views.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Flyinline.Application.Interfaces
{
    public interface ICommonDbContext
    {
        DbSet<Claim> Claim { get; set; }
        DbSet<Flyinline.Domain.Entities.Common.Principal> Principal { get; set; }
        DbSet<Role> Role { get; set; }
        DbSet<RolePermission> RolePermission { get; set; }
        DbSet<PrincipalHasRole> PrincipalHasRole { get; set; }
        DbSet<PrincipalPermission> PrincipalPermission { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        DbQuery<ClaimPermission> ClaimPermission { get; set; }

    }
}
