using System.Threading.Tasks;

namespace MicroERP.Services.Core.Notification
{
    public interface INotificationService
    {
        Task ShowAsync(string message, string title = "");
    }
}
