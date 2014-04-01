using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MicroERP.Business.Domain.Exceptions;
using MicroERP.Business.Domain.Repositories;
using MicroERP.Business.Domain.Models;
using Newtonsoft.Json;
using ViHo.Service.Navigation;
using ViHo.Service.Notification;
using ViHo.Json.Extension;
using MicroERP.Business.Core.Services.Interfaces;

namespace MicroERP.Business.Core.ViewModels
{
    public class CustomerWindowViewModel : ViewModelBase, INavigationAware
    {
        #region Properties

        private readonly ICustomerService customerService;
        private readonly INotificationService notificationService;
        private readonly INavigationService navigationService;
        private CustomerModel customer;

        public CustomerModel Customer
        {
            get { return this.customer; }
            set { base.Set<CustomerModel>(ref this.customer, value); }
        }

        #endregion

        #region Commands

        public RelayCommand SaveCustomerCommand
        {
            get;
            private set;
        }

        public RelayCommand CancelCommand
        {
            get;
            private set;
        }

        #endregion

        #region Constructors

        public CustomerWindowViewModel(ICustomerService customerService, INotificationService notificationService, INavigationService windowService)
        {
            this.customerService = customerService;
            this.notificationService = notificationService;
            this.navigationService = windowService;

            this.SaveCustomerCommand = new RelayCommand(onSaveCustomerExecuted, onSaveCustomerCanExecute);
            this.CancelCommand = new RelayCommand(onCloseExecuted);
        }

        #endregion

        #region Commands Implementations

        private bool onSaveCustomerCanExecute()
        {
            // TODO: check if customer has been created or just modified

            return true;
        }

        private void onSaveCustomerExecuted()
        {
            try
            {
                if (this.customer.ID == 0)
                {
                    this.customerService.Create(this.customer);
                }
                else
                {
                    this.customerService.Update(this.customer);
                }
            }
            catch (CustomerAlreadyExistsException)
            {
                this.notificationService.ShowAsync("Fehler", "Kunde existiert bereits.");
            }
            catch (CustomerNotFoundException)
            {
                this.notificationService.ShowAsync("Fehler", "Der Kunde wurde in der Datenbank nicht gefunden.");
            }

            // TODO: notify mainwindow that search box items need to refresh?

            this.onCloseExecuted();
        }

        private void onCloseExecuted()
        {
            if (navigationService is IFrameNavigationService)
            {
                (navigationService as IFrameNavigationService).BackCommand.Execute(null);
            }
            else if (navigationService is IWindowNavigationService)
            {
                (navigationService as IWindowNavigationService).Close(this);
            }
        }
        
        #endregion

        #region INavigationAware

        public async void OnNavigatedTo(object argument, NavigationType navigationMode)
        {
            var jsonString = argument as string;
            if (jsonString != null)
            {
                this.Customer = await jsonString.FromJson<CustomerModel>(new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.Auto });
            }
        }

        public void OnNavigatedFrom()
        {
            return;
        }

        #endregion
    }
}
