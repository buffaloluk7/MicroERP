namespace MicroERP.Data.Api.Configuration.Interfaces
{
    public interface IApiConfiguration
    {
        string Protocol { get; }

        string Host { get; }

        int Port { get; }

        string Path { get; set; }
    }
}
