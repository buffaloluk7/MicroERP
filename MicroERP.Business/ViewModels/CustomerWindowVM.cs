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
    public class CustomerWindowVM : ViewModelBase
    {
        #region Properties

        private readonly IDataAccessLayer dataAccessLayer;
        private readonly IMessageService messageService;
        private readonly IWindowService windowService;

        #endregion

        #region Commands

        public RelayCommand SaveCustomerCommand
        {
            get;
            private set;
        }

        #endregion

        #region Constructors

        public CustomerWindowVM(IDataAccessLayer dataAccessLayer, IMessageService messageService, IWindowService windowService)
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
    }
}
