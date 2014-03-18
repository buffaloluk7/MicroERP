using MicroERP.Services.Core.Notification;
using System;
using Windows.UI.Popups;

namespace MicroERP.Services.WinRT.Notification
{
    public class NotificationService : INotificationService
    {
        public async void Show(string title, string message)
        {
            await new MessageDialog(message, title).ShowAsync();
        }
    }
}
