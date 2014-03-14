using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MicroERP.Business.DataAccessLayer.Interfaces;
using MicroERP.Business.Models;
using MicroERP.Business.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            throw new NotImplementedException();
        }

        private void onSaveCustomerExecuted()
        {
            throw new NotImplementedException();
        }
        
        #endregion

        public void OnNavigatedTo(object argument)
        {
            this.Customer = (Customer)argument;
        }
    }
}
