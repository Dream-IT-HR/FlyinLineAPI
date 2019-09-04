using System;
using System.Collections.Generic;

namespace Flyinline.Persistance.Models
{
    public partial class Customization
    {
        public int Id { get; set; }
        public string CustomizationKey { get; set; }
        public DateTime Created { get; set; }
        public bool Active { get; set; }
        public string Description { get; set; }
    }
}
