using System;

namespace MicroERP.Business.Domain.Exceptions
{
    public abstract class CustomerException : Exception
    {
        protected CustomerException(string message = null, Exception inner = null) : base(message, inner)
        {
        }
    }
}