using System;
using System.Collections.Generic;

namespace Flyinline.Domain.Entities
{
    public partial class LineStatus
    {
        public Guid Id { get; set; }
        public bool IsReadyForWork { get; set; }

        public virtual Line IdNavigation { get; set; }
    }
}
