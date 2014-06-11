using System;
using Luvi.Http;
using MicroERP.Data.Api.Configuration.Interfaces;
using Newtonsoft.Json;

namespace MicroERP.Data.Api.Repositories
{
    public abstract class ApiRepositoryBase
    {
        #region Fields

        protected readonly JsonSerializerSettings jsonSettings;
        protected readonly RESTRequest request;

        #endregion

        #region Properties

        protected string ConnectionString { get; private set; }

        #endregion

        #region Constructors

        protected ApiRepositoryBase(IApiConfiguration configuration, string path = "")
        {
            this.ConnectionString = string.Format("{0}://{1}:{2}{3}{4}",
                configuration.Protocol,
                configuration.Host,
                configuration.Port,
                configuration.Path,
                path);

            this.jsonSettings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Objects,
                DateTimeZoneHandling = DateTimeZoneHandling.Utc,
                DateFormatHandling = DateFormatHandling.IsoDateFormat
            };

            this.request = new RESTRequest
            {
                Timeout = new TimeSpan(0, 0, 150),
                UseTransferEncodingChunked = false,
                SerializationSettings = this.jsonSettings,
                UseJsonSerialization = true,
                UseContinueHeader = false
            };
        }

        #endregion
    }
}