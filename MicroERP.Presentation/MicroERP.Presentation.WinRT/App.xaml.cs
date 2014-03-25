using MicroERP.Business.Core;
using MicroERP.Business.Core.ViewModels;
using MicroERP.Presentation.WinRT.Views;
using Ninject;
using System;
using System.Collections.Generic;
using ViHo.WinRT.Service.Browsing;
using ViHo.WinRT.Service.Navigation;
using ViHo.WinRT.Service.Notification;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace MicroERP.Presentation.WinRT
{
    sealed partial class App : Application
    {
        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;
        }

        private void OnSuspending(object sender, Windows.ApplicationModel.SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            deferral.Complete();
        }

        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;

            if (rootFrame == null)
            {
                rootFrame = new Frame();
                rootFrame.Language = Windows.Globalization.ApplicationLanguages.Languages[0];

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                }

                Window.Current.Content = rootFrame;

                Dictionary<Type, Type> viewViewModelMapper = new Dictionary<Type, Type>();
                viewViewModelMapper.Add(typeof(MainWindowViewModel), typeof(MainPage));
                viewViewModelMapper.Add(typeof(CustomerWindowViewModel), typeof(CustomerPage));

                var locator = App.Current.Resources["Locator"] as ViewModelLocator;
                var navigationService = new NavigationService(viewViewModelMapper);

                locator.Register(new StandardKernel(), navigationService, new NotificationService(), new BrowsingService());
            }

            if (rootFrame.Content == null)
            {
                rootFrame.Navigate(typeof(MainPage), e.Arguments);
            }

            Window.Current.Activate();
        }
    }
}
