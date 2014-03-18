using GalaSoft.MvvmLight.Ioc;
using MicroERP.Business.ViewModels;
using MicroERP.Presentation.Views;
using MicroERP.Services.Core.Browser;
using MicroERP.Services.Core.Navigation;
using MicroERP.Services.Core.Notification;
using MicroERP.Services.WPF.Browser;
using MicroERP.Services.WPF.Navigation;
using MicroERP.Services.WPF.Notification;
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
            SimpleIoc.Default.Register<INavigationService, NavigationService>();
            SimpleIoc.Default.Register<INotificationService, NotificationService>();
            SimpleIoc.Default.Register<IBrowsingService, BrowsingService>();

            NavigationService.Mapper.Add(typeof(MainWindowViewModel), typeof(MainWindow));
            NavigationService.Mapper.Add(typeof(CustomerWindowViewModel), typeof(CustomerWindow));
        }
    }
}
