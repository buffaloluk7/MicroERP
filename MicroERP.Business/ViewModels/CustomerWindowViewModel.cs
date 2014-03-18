using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MicroERP.Domain.Exceptions;
using MicroERP.Domain.Interfaces;
using MicroERP.Domain.Models;
using MicroERP.Services.Core.Navigation;
using MicroERP.Services.Core.Notification;
using MicroERP.Business.Common;
using Newtonsoft.Json;

namespace MicroERP.Business.ViewModels
{
    public class CustomerWindowViewModel : ViewModelBase, INavigationAware
    {
        #region Properties

        private readonly IRepository repository;
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

        public CustomerWindowViewModel(IRepository repository, INotificationService notificationService, INavigationService windowService)
        {
            this.repository = repository;
            this.notificationService = notificationService;
            this.navigationService = windowService;

            this.SaveCustomerCommand = new RelayCommand(onSaveCustomerExecuted, onSaveCustomerCanExecute);
            this.CancelCommand = new RelayCommand(onCancelExecuted);
        }

        #endregion

        #region Commands Implementations

        private bool onSaveCustomerCanExecute()
        {
            // TODO: check if input has changed (maybe store old customer object to compare?)
            return true;
        }

        private void onSaveCustomerExecuted()
        {
            try
            {
                this.repository.UpdateCustomer(this.customer);
            }
            catch (CustomerNotFoundException)
            {
                this.notificationService.Show("Fehler", "Der Kunde wurde in der Datenbank nicht gefunden.");
            }
        }

        private void onCancelExecuted()
        {
            this.navigationService.Close(this, "Geänderte Daten werden nicht gespeichert.");
        }
        
        #endregion

        public void OnNavigatedTo(object argument)
        {
            if (argument != null)
            {
                this.Customer = JsonConvert.DeserializeObject<CustomerModel>((string)argument, new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.Auto });
            }
        }
    }
}
