using System;
using System.Collections.Generic;

namespace Flyinline.Domain.Entities
{
    public partial class Revision
    {
        public Guid Id { get; set; }
        public string RevisionKey { get; set; }
        public DateTime Created { get; set; }
        public int Granulation { get; set; }
        public int ObjectTypeOrdinal { get; set; }
        public string ObjectTypeName { get; set; }
        public string RevisionType { get; set; }
        public string ModuleKey { get; set; }
        public string SchemaName { get; set; }
        public string SchemaObjectName { get; set; }
        public string ObjectName { get; set; }
        public DateTime Executed { get; set; }
        public string ObjectFullName { get; set; }
        public string Description { get; set; }
    }
}
