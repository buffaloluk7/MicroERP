using MicroERP.Business.Core;
using MicroERP.Business.Core.ViewModels;
using MicroERP.Presentation.Views;
using Ninject;
using System;
using System.Collections.Generic;
using System.Windows;
using ViHo.WPF.Service.Browsing;
using ViHo.WPF.Service.Navigation;
using ViHo.WPF.Service.Notification;

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
