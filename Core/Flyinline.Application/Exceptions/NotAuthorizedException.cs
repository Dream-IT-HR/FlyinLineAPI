using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;

namespace Northwind.Application.Exceptions
{
    public class NotAuthorizedException : Exception
    {
        public NotAuthorizedException()
            : base("Not authorized.")
        {

        }
    }
}
