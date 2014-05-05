using Luvi.WPF.Service.Browsing;
using Luvi.WPF.Service.Navigation;
using Luvi.WPF.Service.Notification;
using MicroERP.Business.Core;
using MicroERP.Business.Core.ViewModels;
using MicroERP.Presentation.Views;
using System;
using System.Collections.Generic;
using System.Windows;

namespace MicroERP.Presentation
{
    /// <summary>
    /// Interaktionslogik für "App.xaml"
    /// </summary>
    public partial class App : Application
    {
        #region Constructors

        public App()
        {
            this.Dispatcher.UnhandledException += Dispatcher_UnhandledException;
            this.Navigating += App_Navigating;
        }

        #endregion

        #region Global Exception Handler

        private void Dispatcher_UnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            e.Handled = true;
            string errorMessage = string.Format("An application error occurred.\n" +
                                                 "Please check whether your data is correct and repeat the action." +
                                                 "If this error occurs again there seems to be a more serious malfunction" +
                                                 "in the application, and you better close it.\n\nError :{0}\n\nDo you want to continue?\n" +
                                                 "(if you click Yes you will continue with your work, if you click No the application will close)",
                                                 e.Exception.Message + (e.Exception.InnerException != null ? "\n" +
                                                 e.Exception.InnerException.Message : null));

            if (MessageBox.Show(errorMessage, "Application Error", MessageBoxButton.YesNoCancel, MessageBoxImage.Error) == MessageBoxResult.No)
            {
                if (MessageBox.Show("WARNING: The application will close. Any changes will not be saved!\nDo you really want to close it?", "Close the application!",
                                    MessageBoxButton.YesNoCancel, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    Application.Current.Shutdown();
                }
            }
        }

        #endregion

        #region App Navigating

        private void App_Navigating(object sender, System.Windows.Navigation.NavigatingCancelEventArgs e)
        {
            Dictionary<Type, Type> viewViewModelMapper = new Dictionary<Type, Type>();
            viewViewModelMapper.Add(typeof(MainWindowViewModel), typeof(MainWindow));
            viewViewModelMapper.Add(typeof(CustomerWindowViewModel), typeof(CustomerWindow));

            var locator = App.Current.Resources["Locator"] as ViewModelLocator;
            var navigationService = new NavigationService(viewViewModelMapper);

            locator.Register(navigationService, new NotificationService(), new BrowsingService());

            this.Navigating -= App_Navigating;
        }

        #endregion
    }
}
