using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MicroERP.Business.DataAccessLayer.Exceptions;
using MicroERP.Business.DataAccessLayer.Interfaces;
using MicroERP.Business.Services.Interfaces;
using MicroERP.Business.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System;

namespace MicroERP.Business.ViewModels
{
    public class MainWindowVM : ViewModelBase
    {
        #region Properties

        private readonly IDataAccessLayer dataAccessLayer;
        private readonly IMessageService messageService;
        private readonly IWindowService windowService;

        private string query = "";
        private IEnumerable<Customer> customers;

        public string Query
        {
            get { return this.query; }
            set 
            {
                base.Set<string>(ref this.query, value);
                this.SearchCommand.RaiseCanExecuteChanged();
            }
        }

        public IEnumerable<Customer> Customers
        {
            get { return this.customers; }
            set { base.Set<IEnumerable<Customer>>(ref this.customers, value); }
        }
        
        #endregion

        #region Commands

        public RelayCommand SearchCommand
        {
            get;
            private set;
        }

        public RelayCommand RepositoryCommand
        {
            get;
            private set;
        }

        public RelayCommand CreateCustomerCommand
        {
            get;
            private set;
        }

        public RelayCommand<Customer> EditCustomerCommand
        {
            get;
            private set;
        }

        public RelayCommand<Customer> DeleteCustomerCommand
        {
            get;
            private set;
        }

        #endregion

        #region Constructors

        public MainWindowVM(IDataAccessLayer dataAccessLayer, IMessageService messageService, IWindowService windowService)
        {
            this.dataAccessLayer = dataAccessLayer;
            this.messageService = messageService;
            this.windowService = windowService;

            this.SearchCommand = new RelayCommand(onSearchExecuted, onSearchCanExecute);
            this.RepositoryCommand = new RelayCommand(onRepositoryExecuted);
            this.CreateCustomerCommand = new RelayCommand(onCreateCustomerExecuted);
            this.EditCustomerCommand = new RelayCommand<Customer>(onEditCustomerExecuted, onEditCustomerCanExecute);
            this.DeleteCustomerCommand = new RelayCommand<Customer>(onDeleteCustomerExecuted, onDeleteCustomerCanExecute);
        }

        #endregion

        #region Commands Implementation

        private async void onSearchExecuted()
        {
            this.Customers = await this.dataAccessLayer.ReadCustomers(this.query);  
        }

        private bool onSearchCanExecute()
        {
            return !string.IsNullOrWhiteSpace(this.query);
        }

        private void onRepositoryExecuted()
        {
            Process browser = new Process();
            browser.EnableRaisingEvents = true;
            browser.StartInfo.Arguments = "https://github.com/buffaloluk7/micro_erp";
            browser.StartInfo.FileName = "chrome.exe";

            try
            {
                browser.Start();
            }
            catch (System.ComponentModel.Win32Exception)
            {
                browser.StartInfo.FileName = "iexplore.exe";
                browser.Start();
            }
        }

        private void onCreateCustomerExecuted()
        {
            this.windowService.Show<CustomerWindowVM>(true);
        }

        private void onEditCustomerExecuted(Customer customer)
        {
            // open detail view
            throw new NotImplementedException();
        }

        private bool onEditCustomerCanExecute(Customer customer)
        {
            return customer != null;
        }

        private async void onDeleteCustomerExecuted(Customer customer)
        {
            try
            {
                await this.dataAccessLayer.DeleteCustomer(customer.ID);
            }
            catch (CustomerNotFoundException)
            {
                this.messageService.Show("Fehler", "Der Kunde wurde in der Datenbank nicht gefunden.");
            }

            var list = this.customers.ToList();
            list.Remove(customer);
            this.Customers = list;
        }

        private bool onDeleteCustomerCanExecute(Customer customer)
        {
            return customer != null;
        }

        #endregion
    }
}
