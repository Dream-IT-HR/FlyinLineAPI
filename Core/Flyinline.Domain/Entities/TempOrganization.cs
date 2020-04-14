using System;
using System.Collections.Generic;

namespace Flyinline.Domain.Entities
{
    public partial class TempOrganization
    {
        public TempOrganization()
        {
            TempLine = new HashSet<TempLine>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<TempLine> TempLine { get; set; }
    }
}
