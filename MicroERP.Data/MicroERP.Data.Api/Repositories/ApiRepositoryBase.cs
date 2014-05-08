using Luvi.Http;
using MicroERP.Data.Api.Configuration.Interfaces;
using Newtonsoft.Json;
using System;

namespace MicroERP.Data.Api.Repositories
{
    public abstract class ApiRepositoryBase
    {
        #region Fields

        private readonly IApiConfiguration configuration;
        protected readonly JsonSerializerSettings jsonSettings;
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

        public ApiRepositoryBase(IApiConfiguration configuration, string path = "")
        {
            this.configuration = configuration;
            this.configuration.Path += path;

            this.jsonSettings = new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.Objects,
                DateTimeZoneHandling = DateTimeZoneHandling.Utc,
                DateFormatHandling = DateFormatHandling.IsoDateFormat
            };

            this.request = new RESTRequest()
            {
                Timeout = new TimeSpan(0, 0, 15),
                UseTransferEncodingChunked = false,
                SerializationSettings = this.jsonSettings,
                UseJsonSerialization = true,
                UseContinueHeader = false
            };
        }

        #endregion
    }
}
