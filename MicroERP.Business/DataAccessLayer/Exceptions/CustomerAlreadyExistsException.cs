using MicroERP.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroERP.Business.DataAccessLayer.Exceptions
{
    class CustomerAlreadyExistsException : DataAccessLayerBaseException
    {
        public CustomerAlreadyExistsException(Customer customer, string message = "Customer already exists.", Exception inner = null) : base(message, inner) { }
    }
}
