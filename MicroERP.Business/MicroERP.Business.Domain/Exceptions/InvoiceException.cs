using System;

namespace MicroERP.Business.Domain.Exceptions
{
    public abstract class InvoiceException : Exception
    {
        protected InvoiceException(string message = null, Exception inner = null) : base(message, inner)
        {
        }
    }
}