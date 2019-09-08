using Flyinline.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Flyinline.Domain.Views.Common
{
    public class ClaimPermission
    {
        public Guid Id { get; set; }
        public Guid ClaimId { get; set; }
        public Guid? PrincipalId { get; set; }
        public bool CanExecute { get; set; }

        public virtual Claim Claim { get; set; }
        public virtual Principal Principal { get; set; }
    }
}
