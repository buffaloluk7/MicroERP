﻿using System.ComponentModel;
using GalaSoft.MvvmLight;
using MicroERP.Business.Domain.Models;

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

        public decimal Tax
        {
            get { return this.invoiceItem.Tax*100; }
            set { this.invoiceItem.Tax = value/100; }
        }

        internal InvoiceItemModel Model
        {
            get { return this.invoiceItem; }
        }

        #endregion

        #region Constructors

        /// <summary>
        ///     Needed to allow adding new rows in datagrid
        /// </summary>
        public InvoiceItemModelViewModel() : this(new InvoiceItemModel())
        {
        }

        public InvoiceItemModelViewModel(InvoiceItemModel invoiceItem)
        {
            this.invoiceItem = invoiceItem ?? new InvoiceItemModel();
            this.invoiceItem.PropertyChanged += invoiceItem_PropertyChanged;
        }

        #endregion

        #region IsValid

        public bool IsValid()
        {
            return this.Amount > 0
                   && this.UnitPrice > 0
                   && this.Tax > 0
                   && !string.IsNullOrWhiteSpace(this.Name);
        }

        #endregion

        #region PropertyChanged

        private void invoiceItem_PropertyChanged(object sender, PropertyChangedEventArgs e)
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