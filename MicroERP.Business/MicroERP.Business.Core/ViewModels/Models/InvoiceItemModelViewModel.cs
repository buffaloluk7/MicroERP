﻿using GalaSoft.MvvmLight;
using MicroERP.Business.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroERP.Business.Core.ViewModels.Models
{
    public class InvoiceItemModelViewModel : ObservableObject
    {
        #region Fields

        private readonly InvoiceItemModel invoiceItem;

        #endregion

        #region Properties

        public int Amount
        {
            get { return this.invoiceItem.Amount; }
            set { this.invoiceItem.Amount = value; }
        }

        public string Name
        {
            get { return this.invoiceItem.Name; }
            set { this.invoiceItem.Name = value; }
        }

        public decimal UnitPrice
        {
            get { return this.invoiceItem.UnitPrice; }
            set { this.invoiceItem.UnitPrice = value; }
        }

        public double Tax
        {
            get { return this.invoiceItem.Tax; }
            set { this.invoiceItem.Tax = value; }
        }

        public InvoiceItemModel Model
        {
            get { return this.invoiceItem; }
        }

        #endregion

        #region Constructors

        public InvoiceItemModelViewModel(InvoiceItemModel invoiceItem)
        {
            if (invoiceItem == null)
            {
                throw new ArgumentNullException("invoiceItem");
            }

            this.invoiceItem = invoiceItem;
            this.invoiceItem.PropertyChanged += invoiceItem_PropertyChanged;
        }

        #endregion

        #region PropertyChanged

        private void invoiceItem_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Amount":
                case "Name":
                case "UnitPrice":
                case "Tax":
                    base.RaisePropertyChanged(e.PropertyName);
                    break;
            }
        }

        #endregion
    }
}