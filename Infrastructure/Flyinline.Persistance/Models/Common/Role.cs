using System;
using System.Collections.Generic;

namespace Flyinline.Persistance.Models.Common
{
    public partial class Role
    {
        public Role()
        {
            PrincipalHasRole = new HashSet<PrincipalHasRole>();
            RolePermission = new HashSet<RolePermission>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<PrincipalHasRole> PrincipalHasRole { get; set; }
        public virtual ICollection<RolePermission> RolePermission { get; set; }
    }
}
