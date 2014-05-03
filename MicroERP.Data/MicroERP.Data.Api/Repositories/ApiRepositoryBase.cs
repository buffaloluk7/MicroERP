using Luvi.Http;
using MicroERP.Data.Api.Configuration.Interfaces;
using System;

namespace MicroERP.Data.Api.Repositories
{
    public abstract class ApiRepositoryBase
    {
        #region Fields

        private readonly IApiConfiguration configuration;
        protected readonly RESTRequest request;

        #endregion

        #region Properties

        protected string ConnectionString
        {
            get
            {
                return string.Format("{0}://{1}:{2}/{3}", this.configuration.Protocol, this.configuration.Host, this.configuration.Port, this.configuration.Path);
            }
        }

        #endregion

        #region Constructors

        public ApiRepositoryBase(IApiConfiguration configuration)
        {
            this.configuration = configuration;
            this.request = new RESTRequest()
            {
                Timeout = new TimeSpan(0, 0, 15),
                UseTransferEncodingChunked = true
            };
        }

        #endregion
    }
}
