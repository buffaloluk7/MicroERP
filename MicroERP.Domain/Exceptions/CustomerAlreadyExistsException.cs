using MicroERP.Domain.Models;
using System;

namespace MicroERP.Domain.Exceptions
{
    public class CustomerAlreadyExistsException : CustomerException
    {
        public CustomerAlreadyExistsException(CustomerModel customer, string message = "Customer already exists.", Exception inner = null) : base(message, inner) { }
    }
}
