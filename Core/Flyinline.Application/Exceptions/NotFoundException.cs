using System;

namespace Northwind.Application.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string name, object key)
            : base($"Entity with the give key: {key} was not found in the repository \"{name}\"")
        {
        }
    }
}