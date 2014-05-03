﻿using MicroERP.Business.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MicroERP.Business.Domain.Repositories
{
    public interface IInvoiceRepository
    {
        Task<InvoiceModel> Create(int customerID, InvoiceModel invoice);

        Task<IEnumerable<InvoiceModel>> Search(int customerID, DateTime? begin = null, DateTime? end = null, double? minPrice = null, double? maxPrice = null);

        Task<InvoiceModel> Read(int customerID, int invoiceID);
    }
}
