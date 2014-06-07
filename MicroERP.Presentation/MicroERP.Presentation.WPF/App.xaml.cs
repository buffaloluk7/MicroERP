using Luvi.Http.Exception;
using Luvi.WPF.Service.Browsing;
using Luvi.WPF.Service.Notification;
using MicroERP.Business.Core;
using MicroERP.Business.Core.ViewModels.Customer;
using MicroERP.Business.Core.ViewModels.Invoice;
using MicroERP.Business.Core.ViewModels.Main;
using MicroERP.Data.Api.Configuration;
using MicroERP.Data.Api.Exceptions;
using MicroERP.Presentation.WPF.Properties;
using MicroERP.Presentation.WPF.Views;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Navigation;
using System.Windows.Threading;
using NavigationService = Luvi.WPF.Service.Navigation.NavigationService;

namespace MicroERP.Presentation.WPF
{
    public partial class App : Application
    {
        #region Constructor

        public App()
        {
            this.DispatcherUnhandledException += Dispatcher_UnhandledException;
            this.Navigating += App_Navigating;
        }

        #endregion

        #region Global Exception Handler

        private void Dispatcher_UnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            var knownExceptions = new Dictionary<Type, Func<Exception, string>>
            {
                {typeof (ServerNotAvailableException), ex => "Server not available."},
                {
                    typeof (FaultyMessageException),
                    ex => "Server message could not be parsed:\n\n" + ex.Message
                },
                {
                    typeof (BadResponseException),
                    ex =>
                    {
                        var badResponseException = ex as BadResponseException;
                        return badResponseException != null ? "Server response sent unexpected HttpStatusCode:\n" +
                                                                    badResponseException.StatusCode : null;
                    }
                }
            };

            var exceptionType = e.Exception.GetType();
            if (knownExceptions.ContainsKey(exceptionType))
            {
                e.Handled = true;

                string customMessage = knownExceptions[exceptionType](e.Exception);
                string errorMessage =
                    string.Format(
                        "An application error occured.\n\nCustom message:\n{0}\n\nError message:\n{1}\n\nFurther information:\n{2}\n\nDo you want to continue?",
                        customMessage,
                        e.Exception.Message,
                        (e.Exception.InnerException != null) ? e.Exception.InnerException.Message : null);

                if (
                    MessageBox.Show(errorMessage, "Application Error", MessageBoxButton.YesNoCancel,
                        MessageBoxImage.Error) == MessageBoxResult.No)
                {
                    Current.Shutdown();
                }
            }

            File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "\\microerp.log", e.Exception.StackTrace);
        }

        #endregion

        #region App Navigating

        private void App_Navigating(object sender, NavigatingCancelEventArgs e)
        {
            var viewViewModelMapper = new Dictionary<Type, Type>
            {
                {typeof (MainWindowViewModel), typeof (MainWindow)},
                {typeof (CustomerWindowViewModel), typeof (CustomerWindow)},
                {typeof (InvoiceWindowViewModel), typeof (InvoiceWindow)}
            };

            var locator = Current.Resources["Locator"] as ViewModelLocator;
            if (locator == null)
            {
                throw new InvalidCastException("locator");
            }

            var api = new Uri(Settings.Default.API);
            var apiConifguration = new ApiConfiguration(api.Host, api.Port, api.Scheme, api.PathAndQuery);

            locator.Register(new NavigationService(viewViewModelMapper), new NotificationService(), new BrowsingService(), apiConifguration);
            this.Navigating -= App_Navigating;
        }

        #endregion
    }
}