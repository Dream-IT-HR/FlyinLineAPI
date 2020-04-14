using System;
using System.Collections.Generic;

namespace Flyinline.Domain.Entities
{
    public partial class Country
    {
        public Country()
        {
            LineLocation = new HashSet<LineLocation>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<LineLocation> LineLocation { get; set; }
    }
}
