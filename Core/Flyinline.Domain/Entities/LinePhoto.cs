using System;
using System.Collections.Generic;

namespace Flyinline.Domain.Entities
{
    public partial class LinePhoto
    {
        public Guid Id { get; set; }
        public Guid LineId { get; set; }
        public string PhotoName { get; set; }
        public string PhotoUrl { get; set; }

        public virtual Line Line { get; set; }
    }
}
