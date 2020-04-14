using System;
using System.Collections.Generic;

namespace Flyinline.Domain.Entities
{
    public partial class Organization
    {
        public Organization()
        {
            Line = new HashSet<Line>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Line> Line { get; set; }
    }
}
