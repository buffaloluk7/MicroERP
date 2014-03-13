using System;

namespace MicroERP.Business.DataAccessLayer.Exceptions
{
    public abstract class DataAccessLayerBaseException : Exception
    {
        public DataAccessLayerBaseException(string message = null, Exception inner = null) : base(message, inner) { }
    }
}
