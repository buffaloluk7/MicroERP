using System;

namespace MicroERP.Business.Domain.Exceptions
{
    public class CustomerNotFoundException : CustomerException
    {
        public CustomerNotFoundException(string message = "Customer not found.", Exception inner = null) : base(message, inner) {}
    }
}