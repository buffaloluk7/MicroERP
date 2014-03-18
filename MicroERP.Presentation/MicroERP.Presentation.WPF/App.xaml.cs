using GalaSoft.MvvmLight.Ioc;
using MicroERP.Business;
using MicroERP.Business.ViewModels;
using MicroERP.Presentation.Views;
using MicroERP.Services.Core.Browser;
using MicroERP.Services.Core.Navigation;
using MicroERP.Services.Core.Notification;
using MicroERP.Services.WPF.Browser;
using MicroERP.Services.WPF.Navigation;
using MicroERP.Services.WPF.Notification;
using System;
using System.Collections.Generic;
using System.Windows;
using MicroERP.Presentation.WPF;
using Ninject;

namespace MicroERP.Presentation
{
    /// <summary>
    /// Interaktionslogik für "App.xaml"
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            this.Navigating += App_Navigating;
        }

        void App_Navigating(object sender, System.Windows.Navigation.NavigatingCancelEventArgs e)
        {
            Dictionary<Type, Type> viewViewModelMapper = new Dictionary<Type, Type>();
            viewViewModelMapper.Add(typeof(MainWindowViewModel), typeof(MainWindow));
            viewViewModelMapper.Add(typeof(CustomerWindowViewModel), typeof(CustomerWindow));

            var locator = App.Current.Resources["Locator"] as ViewModelLocator;
            var navigationService = new NavigationService(viewViewModelMapper);

            locator.Register(new StandardKernel(), navigationService, new NotificationService(), new BrowsingService());

            this.Navigating -= App_Navigating;
        }
    }
}
