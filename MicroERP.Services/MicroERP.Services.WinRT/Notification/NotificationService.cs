using MicroERP.Services.Core.Notification;
using System;
using Windows.UI.Popups;

namespace MicroERP.Services.WinRT.Notification
{
    public class NotificationService : INotificationService
    {
        public async void ShowAsync(string message, string title = "")
        {
            await new MessageDialog(message, title).ShowAsync();
        }
    }
}
