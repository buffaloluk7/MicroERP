using MicroERP.Business.Domain.Models;
using System;

namespace MicroERP.Business.Domain.Exceptions
{
    public class InvoiceAlreadyExistsException : InvoiceException
    {
        public InvoiceAlreadyExistsException(InvoiceModel invoice, string message = "Invoice already exists.", Exception inner = null) : base(message, inner) { }
    }
}
