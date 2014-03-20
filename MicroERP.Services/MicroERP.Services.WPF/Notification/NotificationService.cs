using MicroERP.Services.Core.Notification;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MicroERP.Services.WPF.Notification
{
    public class NotificationService : INotificationService
    {
        public async Task ShowAsync(string message, string title = "")
        {
            await Task.Run(() =>
            {
                MessageBox.Show(message, title);
            });
        }
    }
}