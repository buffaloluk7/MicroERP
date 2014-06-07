using MicroERP.Data.Api.Configuration.Interfaces;

namespace MicroERP.Data.Api.Configuration
{
    public class ApiConfiguration : IApiConfiguration
    {
        #region Properties

        public string Protocol { get; private set; }

        public string Host { get; private set; }

        public int Port { get; private set; }

        public string Path { get; set; }

        #endregion

        #region Constructors

        public ApiConfiguration(string host, int port, string protocol = "http", string path = "")
        {
            this.Protocol = protocol;
            this.Host = host;
            this.Port = port;
            this.Path = path;
        }

        #endregion
    }
}