using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroERP.Business.DataAccessLayer.Exceptions
{
    public abstract class DataAccessLayerBaseException : Exception
    {
        public DataAccessLayerBaseException(string message = null, Exception inner = null) : base(message, inner) { }
    }
}
