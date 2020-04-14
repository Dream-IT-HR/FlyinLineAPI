using System;
using System.Collections.Generic;

namespace Flyinline.Domain.Entities
{
    public partial class LineLocation
    {
        public Guid Id { get; set; }
        public string City { get; set; }
        public Guid CountryId { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string State { get; set; }
        public string StreetAddress { get; set; }
        public string ZipCode { get; set; }

        public virtual Country Country { get; set; }
        public virtual Line IdNavigation { get; set; }
    }
}
