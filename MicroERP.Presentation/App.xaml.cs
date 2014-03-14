using MicroERP.Business.Services;
using MicroERP.Business.ViewModels;
using MicroERP.Presentation.Views;
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
            NavigationService.Mapper.Add(typeof(MainWindowVM), typeof(MainWindow));
            NavigationService.Mapper.Add(typeof(CustomerWindowVM), typeof(CustomerWindow));
        }
    }
}
