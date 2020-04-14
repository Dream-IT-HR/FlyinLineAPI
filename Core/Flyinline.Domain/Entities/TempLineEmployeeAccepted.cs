using System;
using System.Collections.Generic;

namespace Flyinline.Domain.Entities
{
    public partial class TempLineEmployeeAccepted
    {
        public Guid Id { get; set; }
        public DateTime InviteAcceptedOn { get; set; }
    }
}
