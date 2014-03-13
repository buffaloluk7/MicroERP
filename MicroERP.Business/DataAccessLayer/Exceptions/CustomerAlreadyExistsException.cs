using MicroERP.Business.Models;
using System;

namespace MicroERP.Business.DataAccessLayer.Exceptions
{
    class CustomerAlreadyExistsException : DataAccessLayerBaseException
    {
        public CustomerAlreadyExistsException(Customer customer, string message = "Customer already exists.", Exception inner = null) : base(message, inner) { }
    }
}
