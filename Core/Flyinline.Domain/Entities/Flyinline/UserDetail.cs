using System;
using System.Collections.Generic;

namespace Flyinline.Domain.Entities.Flyinline
{
    public partial class UserDetail
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Fullname { get; set; }
        public string Username { get; set; }
    }
}
