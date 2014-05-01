using System;

namespace MicroERP.Business.Domain.Exceptions
{
    public abstract class InvoiceException : Exception
    {
        public InvoiceException(string message = null, Exception inner = null) : base(message, inner) { }
    }
}
