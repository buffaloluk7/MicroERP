using System;

namespace MicroERP.Domain.Exceptions
{
    public abstract class CustomerException : Exception
    {
        public CustomerException(string message = null, Exception inner = null) : base(message, inner) { }
    }
}
