using System.Threading.Tasks;

namespace MicroERP.Services.Core.Browser
{
    public interface IBrowsingService
    {
        Task OpenLinkAsync(string url);
    }
}
