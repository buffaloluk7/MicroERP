using Luvi.Http;
using System;

namespace MicroERP.Data.Api.Repositories
{
    public abstract class ApiRepositoryBase
    {
        #region Fields

        protected RESTRequest request;

        #endregion

        #region Constructors

        public ApiRepositoryBase()
        {
            this.request = new RESTRequest()
            {
                Timeout = new TimeSpan(0, 0, 15),
                UseTransferEncodingChunked = true
            };
        }

        #endregion
    }
}
