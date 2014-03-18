using MicroERP.Services.Core.Browser;
using System;
using Windows.System;

namespace MicroERP.Services.WinRT.Browser
{
    public class BrowsingService : IBrowsingService
    {
        public async void OpenLink(string url)
        {
            await Launcher.LaunchUriAsync(new Uri(url));
        }
    }
}
