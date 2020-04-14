using System;
using System.Collections.Generic;

namespace Flyinline.Domain.Entities
{
    public partial class BusinessDay
    {
        public BusinessDay()
        {
            LineBusinessHour = new HashSet<LineBusinessHour>();
        }

        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string ShortName { get; set; }

        public virtual ICollection<LineBusinessHour> LineBusinessHour { get; set; }
    }
}
