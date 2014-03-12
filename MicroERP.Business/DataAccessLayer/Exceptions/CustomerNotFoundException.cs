using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MicroERP.Business.DataAccessLayer.Exceptions
{
    class CustomerNotFoundException : DataAccessLayerBaseException
    {
        public CustomerNotFoundException(string message = "Customer not found.", Exception inner = null) : base(message, inner) {}
    }
}