using System;
using System.Collections.Generic;

namespace Flyinline.Domain.Entities.Common
{
    public partial class Principal
    {
        public Principal()
        {
            PrincipalHasRole = new HashSet<PrincipalHasRole>();
            PrincipalPermission = new HashSet<PrincipalPermission>();
        }

        public Guid Id { get; set; }
        public bool SuperAdmin { get; set; }
        public string Username { get; set; }

        public virtual ICollection<PrincipalHasRole> PrincipalHasRole { get; set; }
        public virtual ICollection<PrincipalPermission> PrincipalPermission { get; set; }
    }
}
