using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MicroERP.Business.DataAccessLayer.Exceptions;
using MicroERP.Business.DataAccessLayer.Interfaces;
using MicroERP.Business.Interfaces;
using MicroERP.Business.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace MicroERP.Business.VVM
{
    public class MainVVM : ViewModelBase
    {
        #region Properties

        private readonly IDataAccessLayer dataAccessLayer;
        private readonly IMessageService messageService;

        private string firstName = "";
        private string lastName = "";
        private string company = "";
        private IEnumerable<Customer> customers;

        public string FirstName
        {
            get { return this.firstName; }
            set 
            { 
                base.Set<string>(ref this.firstName, value);
                this.SearchCommand.RaiseCanExecuteChanged();
            }
        }

        public string LastName
        {
            get { return this.lastName; }
            set
            {
                base.Set<string>(ref this.lastName, value);
                this.SearchCommand.RaiseCanExecuteChanged();
            }
        }

        public string Company
        {
            get { return this.company; }
            set
            {
                base.Set<string>(ref this.company, value);
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

        public RelayCommand<Customer> DeleteCustomerCommand
        {
            get;
            private set;
        }

        #endregion

        #region Constructors

        public MainVVM(IDataAccessLayer dataAccessLayer, IMessageService messageService)
        {
            this.dataAccessLayer = dataAccessLayer;
            this.SearchCommand = new RelayCommand(onSearchExecuted, onSearchCanExecute);
            this.RepositoryCommand = new RelayCommand(onRepositoryExecuted);
            this.DeleteCustomerCommand = new RelayCommand<Customer>(onDeleteCustomerExecuted, onDeleteCustomerCanExecute);
        }

        #endregion

        #region Command Implementation

        private async void onSearchExecuted()
        {
            this.Customers = await this.dataAccessLayer.ReadCustomers(this.firstName, this.lastName, this.company);  
        }

        private bool onSearchCanExecute()
        {
            return !(string.IsNullOrWhiteSpace(firstName)
                && string.IsNullOrWhiteSpace(lastName)
                && string.IsNullOrWhiteSpace(company));
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

        private async void onDeleteCustomerExecuted(Customer customer)
        {
            try
            {
                await this.dataAccessLayer.DeleteCustomer(customer.ID);
            }
            catch (CustomerNotFoundException)
            {
                messageService.Show("Fehler", "Benutzer nicht gefunden.");
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
