using System;

namespace MicroERP.Business.Domain.Exceptions
{
    public class InvoiceNotFoundException : InvoiceException
    {
        public InvoiceNotFoundException(string message = "Invoice not found.", Exception inner = null) : base(message, inner) { }
    }
}