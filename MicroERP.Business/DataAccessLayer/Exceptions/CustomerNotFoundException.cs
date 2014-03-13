using System;

namespace MicroERP.Business.DataAccessLayer.Exceptions
{
    class CustomerNotFoundException : DataAccessLayerBaseException
    {
        public CustomerNotFoundException(string message = "Customer not found.", Exception inner = null) : base(message, inner) {}
    }
}