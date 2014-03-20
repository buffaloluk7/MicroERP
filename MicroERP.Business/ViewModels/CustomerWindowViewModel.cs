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

        private readonly ICustomerRepository repository;
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

        public CustomerWindowViewModel(ICustomerRepository customerRepository, INotificationService notificationService, INavigationService windowService)
        {
            this.repository = customerRepository;
            this.notificationService = notificationService;
            this.navigationService = windowService;

            this.SaveCustomerCommand = new RelayCommand(onSaveCustomerExecuted, onSaveCustomerCanExecute);
            this.CancelCommand = new RelayCommand(onCancelExecuted);
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
                this.repository.UpdateCustomer(this.customer);
            }
            catch (CustomerNotFoundException)
            {
                this.notificationService.ShowAsync("Fehler", "Der Kunde wurde in der Datenbank nicht gefunden.");
            }

            // TODO: notify mainwindow that search box items need to refresh?

            this.navigationService.Close(this);
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
