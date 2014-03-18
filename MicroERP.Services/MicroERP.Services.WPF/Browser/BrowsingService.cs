using MicroERP.Services.Core.Browser;
using System.Diagnostics;

namespace MicroERP.Services.WPF.Browser
{
    public class BrowsingService : IBrowsingService
    {
        public void OpenLink(string url)
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
        }
    }
}
