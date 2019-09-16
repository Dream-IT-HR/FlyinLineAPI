using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flyinline.WebUI.Models
{
    public class GoogleAuthenticateRequest
    {
        public string TokenId { get; set; } // jwt token goole gave to client
    }
}
