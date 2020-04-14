using System;
using System.Collections.Generic;

namespace Flyinline.Domain.Entities
{
    public partial class LineEmployee
    {
        public Guid Id { get; set; }
        public Guid EmployeeId { get; set; }
        public Guid LineId { get; set; }

        public virtual UserDetail Employee { get; set; }
        public virtual Line Line { get; set; }
        public virtual LineEmployeeAccepted LineEmployeeAccepted { get; set; }
        public virtual LineEmployeeInvited LineEmployeeInvited { get; set; }
    }
}
