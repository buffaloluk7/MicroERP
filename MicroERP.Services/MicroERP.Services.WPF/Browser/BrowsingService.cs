using MicroERP.Services.Core.Browser;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MicroERP.Services.WPF.Browser
{
    public class BrowsingService : IBrowsingService
    {
        public async Task OpenLinkAsync(string url)
        {
            if (url == null)
            {
                throw new ArgumentException("Navigation URL cannot be null");
            }

            await Task.Run(() =>
            {
                Process browser = new Process();
                browser.EnableRaisingEvents = true;
                browser.StartInfo.Arguments = url;
                browser.StartInfo.FileName = "chrome.exe";

                try
                {
                    browser.Start();
                }
                catch (System.ComponentModel.Win32Exception)
                {
                    browser.StartInfo.FileName = "iexplore.exe";
                    browser.Start();
                }
            });
        }
    }
}
