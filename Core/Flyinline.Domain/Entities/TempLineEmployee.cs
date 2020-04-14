using System;
using System.Collections.Generic;

namespace Flyinline.Domain.Entities
{
    public partial class TempLineEmployee
    {
        public Guid Id { get; set; }
        public Guid LineId { get; set; }
        public Guid? EmployeeId { get; set; }
        public string Phone { get; set; }
    }
}
