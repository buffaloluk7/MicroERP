﻿using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Luvi.Json.Extension;
using Luvi.Service.Navigation;
using Luvi.Service.Notification;
using MicroERP.Business.Core.Factories;
using MicroERP.Business.Core.Services.Interfaces;
using MicroERP.Business.Core.ViewModels.Customers;
using MicroERP.Business.Domain.Enums;
using MicroERP.Business.Domain.Models;
using Microsoft.Practices.Unity;
using Newtonsoft.Json;
using System;
using System.Linq;

namespace MicroERP.Business.Core.ViewModels
{
    public class CustomerWindowViewModel : ObservableObject, INavigationAware
    {
        #region Fields

        private readonly IUnityContainer container;
        private readonly ICustomerService customerService;
        private readonly INotificationService notificationService;
        private readonly INavigationService navigationService;

        #endregion

        #region Propterties

        public CustomerDataViewModel CustomerData
        {
            get;
            private set;
        }

        #endregion

        #region Commands

        public RelayCommand CancelCommand
        {
            get;
            private set;
        }

        #endregion

        #region Constructors

        public CustomerWindowViewModel(IUnityContainer container, ICustomerService customerService, INotificationService notificationService, INavigationService navigationService)
        {
            this.container = container;
            this.customerService = customerService;
            this.notificationService = notificationService;
            this.navigationService = navigationService;

            this.CancelCommand = new RelayCommand(onCancelExecuted);

#if DEBUG
            if (ViewModelBase.IsInDesignModeStatic)
            {
                var customer = this.customerService.Read("lukas").ContinueWith((t) =>
                {
                    this.OnNavigatedTo(t.Result.First().ToJson(new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.Objects }), NavigationType.Forward);
                });
            }
#endif
        }

        #endregion

        #region Command Implementations

        private void onCancelExecuted()
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
            var customerRaw = argument as string;
            CustomerModel customer = null;

            if (argument is CustomerType)
            {
                customer = CustomerModelFactory.FromType((CustomerType)argument);
            }
            else if (customerRaw != null)
            {
                customer = await customerRaw.FromJsonAsync<CustomerModel>(new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.Objects });
            }
            else
            {
                throw new ArgumentOutOfRangeException("argument");
            }

            this.CustomerData = this.container.Resolve<CustomerDataViewModel>(new ParameterOverride("customer", customer));
            this.RaisePropertyChanged(() => this.CustomerData);
        }

        public void OnNavigatedFrom() { }

        #endregion
    }
}