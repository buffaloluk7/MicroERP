using MicroERP.Business.Services;
using MicroERP.Business.ViewModels;
using System.Windows;

namespace MicroERP.Presentation
{
    /// <summary>
    /// Interaktionslogik für "App.xaml"
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            WindowService.Mapper.Add(typeof(MainWindowVM), typeof(MainWindow));
            WindowService.Mapper.Add(typeof(CustomerWindowVM), typeof(CustomerWindow));
        }
    }
}
