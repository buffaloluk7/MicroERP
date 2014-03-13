using MicroERP.Business.Services.Interfaces;
using System.Windows.Forms;

namespace MicroERP.Business.Services
{
    public class MessageService : IMessageService
    {
        public void Show(string title, string message)
        {
            MessageBox.Show(message, title);
        }
    }
}
