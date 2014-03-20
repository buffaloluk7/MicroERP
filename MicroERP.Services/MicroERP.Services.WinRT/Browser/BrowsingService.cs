using MicroERP.Services.Core.Browser;
using System;
using System.Threading.Tasks;
using Windows.System;

namespace MicroERP.Services.WinRT.Browser
{
    public class BrowsingService : IBrowsingService
    {
        public async Task OpenLinkAsync(string url)
        {
            if (url == null)
            {
                throw new ArgumentException("Navigation URL cannot be null");
            }

            await Launcher.LaunchUriAsync(new Uri(url));
        }
    }
}
