using System;
using System.Collections.Generic;

namespace Flyinline.Domain.Entities
{
    public partial class UserDetail
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Fullname { get; set; }
        public string Username { get; set; }
        public string Nickname { get; set; }

        public virtual Principal IdNavigation { get; set; }
    }
}
