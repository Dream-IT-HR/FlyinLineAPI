using System;
using System.Collections.Generic;

namespace Flyinline.Domain.Entities
{
    public partial class LineEmployeeAccepted
    {
        public Guid Id { get; set; }
        public DateTime InviteAcceptedOn { get; set; }

        public virtual LineEmployee IdNavigation { get; set; }
    }
}
