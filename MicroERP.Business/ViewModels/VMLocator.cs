﻿using GalaSoft.MvvmLight.Ioc;
using MicroERP.Business.DataAccessLayer.Interfaces;
using MicroERP.Business.Factory;
using MicroERP.Business.Services;
using MicroERP.Business.Services.Interfaces;

namespace MicroERP.Business.ViewModels
{
    public sealed class VMLocator
    {
        public VMLocator()
        {
            if (!SimpleIoc.Default.IsRegistered<IDataAccessLayer>())
            {
                SimpleIoc.Default.Register<IDataAccessLayer>(DataAccessLayerFactory.CreateDataAccessLayer);
            }

            if (!SimpleIoc.Default.IsRegistered<IMessageService>())
            {
                SimpleIoc.Default.Register<IMessageService, MessageService>();
            }

            if (!SimpleIoc.Default.IsRegistered<IWindowService>())
            {
                SimpleIoc.Default.Register<IWindowService, WindowService>();
            }

            if (!SimpleIoc.Default.IsRegistered<MainWindowVM>())
            {
                SimpleIoc.Default.Register<MainWindowVM>();
            }

            if (!SimpleIoc.Default.IsRegistered<CustomerWindowVM>())
            {
                SimpleIoc.Default.Register<CustomerWindowVM>();
            }
        }

        public MainWindowVM Main
        {
            get { return SimpleIoc.Default.GetInstance<MainWindowVM>(); }
        }

        public CustomerWindowVM CustomerDetail
        {
            get { return SimpleIoc.Default.GetInstance<CustomerWindowVM>(); }
        }
    }
}