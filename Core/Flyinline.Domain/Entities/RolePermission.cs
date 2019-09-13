using System;
using System.Collections.Generic;

namespace Flyinline.Domain.Entities
{
    public partial class RolePermission
    {
        public Guid Id { get; set; }
        public Guid? ClaimId { get; set; }
        public Guid RoleId { get; set; }
        public bool CanExecute { get; set; }

        public virtual Claim Claim { get; set; }
        public virtual Role Role { get; set; }
    }
}
