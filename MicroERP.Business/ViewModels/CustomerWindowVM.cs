using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MicroERP.Business.Common;
using MicroERP.Business.DataAccessLayer.Exceptions;
using MicroERP.Business.DataAccessLayer.Interfaces;
using MicroERP.Business.Models;
using MicroERP.Business.Services.Interfaces;

namespace MicroERP.Business.ViewModels
{
    public class CustomerWindowVM : ViewModelBase, INavigationAware
    {
        #region Properties

        private readonly IDataAccessLayer dataAccessLayer;
        private readonly IMessageService messageService;
        private readonly INavigationService windowService;
        private Customer customer;

        public Customer Customer
        {
            get { return this.customer; }
            set { base.Set<Customer>(ref this.customer, value); }
        }

        #endregion

        #region Commands

        public RelayCommand SaveCustomerCommand
        {
            get;
            private set;
        }

        #endregion

        #region Constructors

        public CustomerWindowVM(IDataAccessLayer dataAccessLayer, IMessageService messageService, INavigationService windowService)
        {
            this.dataAccessLayer = dataAccessLayer;
            this.messageService = messageService;
            this.windowService = windowService;

            this.SaveCustomerCommand = new RelayCommand(onSaveCustomerExecuted, onSaveCustomerCanExecute);
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
                this.dataAccessLayer.UpdateCustomer(this.customer);
            }
            catch (CustomerNotFoundException)
            {
                this.messageService.Show("Fehler", "Der Kunde wurde in der Datenbank nicht gefunden.");
            }
        }
        
        #endregion

        public void OnNavigatedTo(object argument)
        {
            this.Customer = (argument as Customer).Clone();
        }
    }
}
