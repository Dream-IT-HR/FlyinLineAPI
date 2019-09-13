using System;
using System.Collections.Generic;

namespace Flyinline.Domain.Entities
{
    public partial class Module
    {
        public int Id { get; set; }
        public string ClassName { get; set; }
        public string DefaultSchemaName { get; set; }
        public string Name { get; set; }
        public DateTime Created { get; set; }
        public bool Active { get; set; }
        public string Description { get; set; }
    }
}
