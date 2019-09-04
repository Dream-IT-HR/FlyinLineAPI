using System;
using System.Collections.Generic;

namespace Flyinline.Persistance.Models
{
    public partial class PrincipalPermission
    {
        public Guid Id { get; set; }
        public Guid ClaimId { get; set; }
        public Guid? PrincipalId { get; set; }
        public bool CanExecute { get; set; }

        public virtual Claim Claim { get; set; }
        public virtual Principal Principal { get; set; }
    }
}
