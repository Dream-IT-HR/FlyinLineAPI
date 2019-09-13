using System;
using System.Collections.Generic;

namespace Flyinline.Domain.Entities
{
    public partial class PrincipalHasRole
    {
        public Guid Id { get; set; }
        public Guid? PrincipalId { get; set; }
        public Guid? RoleId { get; set; }

        public virtual Principal Principal { get; set; }
        public virtual Role Role { get; set; }
    }
}
