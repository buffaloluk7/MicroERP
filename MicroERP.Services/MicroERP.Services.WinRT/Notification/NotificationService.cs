using MicroERP.Services.Core.Notification;
using System;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace MicroERP.Services.WinRT.Notification
{
    public class NotificationService : INotificationService
    {
        public async Task ShowAsync(string message, string title = "")
        {
            await new MessageDialog(message, title).ShowAsync();
        }
    }
}
