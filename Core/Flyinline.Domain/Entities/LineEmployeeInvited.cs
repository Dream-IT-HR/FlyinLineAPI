using System;
using System.Collections.Generic;

namespace Flyinline.Domain.Entities
{
    public partial class LineEmployeeInvited
    {
        public Guid Id { get; set; }
        public string InviteEmail { get; set; }
        public DateTime InviteSentOn { get; set; }

        public virtual LineEmployee IdNavigation { get; set; }
    }
}
