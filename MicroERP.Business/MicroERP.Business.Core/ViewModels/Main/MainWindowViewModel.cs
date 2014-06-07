using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Luvi.Service.Browsing;
using MicroERP.Business.Core.ViewModels.Main.Commands;
using MicroERP.Business.Core.ViewModels.Main.Search;
using Microsoft.Practices.Unity;

namespace MicroERP.Business.Core.ViewModels.Main
{
    public class MainWindowViewModel : ObservableObject
    {
        #region Fields

        private readonly IBrowsingService browsingService;

        private readonly SearchCustomersViewModel searchCustomersViewModel;
        private readonly SearchInvoicesViewModel searchInvoicesViewModel;

        private readonly CustomerCommandsViewModel customerCommandsViewModel;
        private readonly InvoiceCommandsViewModel invoiceCommandsViewModel;

        #endregion

        #region Properties

        public SearchCustomersViewModel SearchCustomersViewModel
        {
            get { return this.searchCustomersViewModel; }
        }

        public SearchInvoicesViewModel SearchInvoicesViewModel
        {
            get { return this.searchInvoicesViewModel; }
        }

        public CustomerCommandsViewModel CustomerCommandsViewModel
        {
            get { return this.customerCommandsViewModel; }
        }

        public InvoiceCommandsViewModel InvoiceCommandsViewModel
        {
            get { return this.invoiceCommandsViewModel; }
        }

        public RelayCommand RepositoryCommand { get; private set; }

        #endregion

        #region Constructor

        public MainWindowViewModel(IUnityContainer container, IBrowsingService browsingService,
            SearchCustomersViewModel searchCustomersViewModel, SearchInvoicesViewModel searchInvoicesViewModel)
        {
            this.browsingService = browsingService;

            this.searchCustomersViewModel = searchCustomersViewModel;
            this.searchInvoicesViewModel = searchInvoicesViewModel;

            this.customerCommandsViewModel =
                container.Resolve<CustomerCommandsViewModel>(new ParameterOverride("searchCustomersViewModel",
                    this.searchCustomersViewModel));
            this.invoiceCommandsViewModel =
                container.Resolve<InvoiceCommandsViewModel>(new ParameterOverride("searchInvoicesViewModel",
                    this.searchInvoicesViewModel));

            this.RepositoryCommand = new RelayCommand(onRepositoryExecuted);
        }

        #endregion

        #region Command Implementations

        private async void onRepositoryExecuted()
        {
            await this.browsingService.OpenLinkAsync("https://github.com/buffaloluk7/micro_erp.git");
        }

        #endregion
    }
}