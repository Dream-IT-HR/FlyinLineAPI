using System;
using System.Collections.Generic;

namespace Flyinline.Persistance.Models
{
    public partial class Claim
    {
        public Claim()
        {
            PrincipalPermission = new HashSet<PrincipalPermission>();
            RolePermission = new HashSet<RolePermission>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<PrincipalPermission> PrincipalPermission { get; set; }
        public virtual ICollection<RolePermission> RolePermission { get; set; }
    }
}
