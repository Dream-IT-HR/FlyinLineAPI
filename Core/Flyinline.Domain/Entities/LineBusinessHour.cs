using System;
using System.Collections.Generic;

namespace Flyinline.Domain.Entities
{
    public partial class LineBusinessHour
    {
        public Guid Id { get; set; }
        public Guid BusinessDayId { get; set; }
        public int EndTime { get; set; }
        public Guid LineId { get; set; }
        public int StartTime { get; set; }

        public virtual BusinessDay BusinessDay { get; set; }
        public virtual Line Line { get; set; }
    }
}
