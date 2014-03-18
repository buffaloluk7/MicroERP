using MicroERP.Services.Core.Notification;
using System.Windows.Forms;

namespace MicroERP.Services.WPF.Notification
{
    public class NotificationService : INotificationService
    {
        public void Show(string title, string message)
        {
            MessageBox.Show(message, title);
        }
    }
}