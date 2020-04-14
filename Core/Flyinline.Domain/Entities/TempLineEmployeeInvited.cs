using System;
using System.Collections.Generic;

namespace Flyinline.Domain.Entities
{
    public partial class TempLineEmployeeInvited
    {
        public Guid Id { get; set; }
        public DateTime InviteSentOn { get; set; }
    }
}
