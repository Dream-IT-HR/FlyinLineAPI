using System;
using System.Collections.Generic;

namespace Flyinline.Domain.Entities
{
    public partial class Line
    {
        public Line()
        {
            LineBusinessHour = new HashSet<LineBusinessHour>();
            LineEmployee = new HashSet<LineEmployee>();
            LinePhoto = new HashSet<LinePhoto>();
        }

        public Guid Id { get; set; }
        public Guid OrganizationId { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }

        public virtual Organization Organization { get; set; }
        public virtual LineLocation LineLocation { get; set; }
        public virtual LineStatus LineStatus { get; set; }
        public virtual ICollection<LineBusinessHour> LineBusinessHour { get; set; }
        public virtual ICollection<LineEmployee> LineEmployee { get; set; }
        public virtual ICollection<LinePhoto> LinePhoto { get; set; }
    }
}
