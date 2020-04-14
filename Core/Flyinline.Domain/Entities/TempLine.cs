using System;
using System.Collections.Generic;

namespace Flyinline.Domain.Entities
{
    public partial class TempLine
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid OrganizationId { get; set; }

        public virtual TempOrganization Organization { get; set; }
    }
}
