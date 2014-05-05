using Luvi.Http;
using Luvi.Json.Converter;
using MicroERP.Business.Domain.Models;
using MicroERP.Data.Api.Configuration.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace MicroERP.Data.Api.Repositories
{
    public abstract class ApiRepositoryBase
    {
        #region Fields

        private readonly IApiConfiguration configuration;
        private readonly Dictionary<string, Type> knownTypes;
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

        public ApiRepositoryBase(IApiConfiguration configuration)
        {
            this.configuration = configuration;

            this.knownTypes = new Dictionary<string, Type>();
            this.knownTypes.Add("person", typeof(PersonModel));
            this.knownTypes.Add("company", typeof(CompanyModel));

            this.jsonSettings = new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.Objects,
                DateTimeZoneHandling = DateTimeZoneHandling.Utc,
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
            };
            this.jsonSettings.Converters.Add(new JsonKnownTypeConverter<CustomerModel>(this.knownTypes));

            this.request = new RESTRequest()
            {
                Timeout = new TimeSpan(0, 0, 15),
                UseTransferEncodingChunked = true,
                SerializationSettings = this.jsonSettings
            };
        }

        #endregion
    }
}
