using Flyinline.Domain.Entities;
using Flyinline.Domain.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Flyinline.Application.Interfaces
{
    public interface IFlyinlineDbContext
    {
        // DatabaseFacade Database { get; }
        DbSet<Claim> Claim { get; set; }
        DbSet<Principal> Principal { get; set; }
        DbSet<Role> Role { get; set; }
        DbSet<RolePermission> RolePermission { get; set; }
        DbSet<PrincipalHasRole> PrincipalHasRole { get; set; }
        DbSet<PrincipalPermission> PrincipalPermission { get; set; }
        DbQuery<ClaimPermission> ClaimPermission { get; set; }

        DbSet<UserDetail> UserDetail{ get; set; }
        
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
