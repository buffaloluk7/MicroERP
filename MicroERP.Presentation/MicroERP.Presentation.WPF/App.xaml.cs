using Luvi.Http.Exception;
using Luvi.WPF.Service.Browsing;
using Luvi.WPF.Service.Navigation;
using Luvi.WPF.Service.Notification;
using MicroERP.Business.Core;
using MicroERP.Business.Core.ViewModels;
using MicroERP.Data.Api.Exceptions;
using MicroERP.Presentation.Views;
using System;
using System.Collections.Generic;
using System.IO;
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
            this.DispatcherUnhandledException += Dispatcher_UnhandledException;
            this.Navigating += App_Navigating;
        }

        #endregion

        #region Global Exception Handler

        private void Dispatcher_UnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            Dictionary<Type, Func<Exception, string>> knownExceptions = new Dictionary<Type, Func<Exception, string>>
            {
                { typeof(ServerNotAvailableException), (ex) => {return "Server not available.";} },
                { typeof(FaultyMessageException), (ex) => {return "Server message could not be parsed:\n\n" + ex.Message;} },
                { typeof(BadResponseException), (ex) => {return "Server response sent unexpected HttpStatusCode:\n" + (ex as BadResponseException).StatusCode;} }
            };

            var exceptionType = e.Exception.GetType();
            if (knownExceptions.ContainsKey(exceptionType))
            {
                e.Handled = true;

                string customMessage = knownExceptions[exceptionType](e.Exception);
                string errorMessage = string.Format("An application error occured.\n\nCustom message:\n{0}\n\nError message:\n{1}\n\nFurther information:\n{2}\n\nDo you want to continue?",
                                                    customMessage,
                                                    e.Exception.Message,
                                                    (e.Exception.InnerException != null) ? e.Exception.InnerException.Message : null);
                
                if (MessageBox.Show(errorMessage, "Application Error", MessageBoxButton.YesNoCancel, MessageBoxImage.Error) == MessageBoxResult.No)
                {
                    Application.Current.Shutdown();
                }
            }

            File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "\\microerp.log", e.Exception.StackTrace);
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
