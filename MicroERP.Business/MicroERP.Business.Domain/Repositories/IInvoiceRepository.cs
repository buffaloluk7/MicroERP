using System.Collections.Generic;
using System.Threading.Tasks;
using MicroERP.Business.Domain.DTO;
using MicroERP.Business.Domain.Exceptions;
using MicroERP.Business.Domain.Models;

namespace MicroERP.Business.Domain.Repositories
{
    public interface IInvoiceRepository
    {
        /// <summary>
        /// Create a new invoice for an existing customer.
        /// </summary>
        /// <param name="customerID">A valid customer ID.</param>
        /// <param name="invoice">InvoiceModel unequal to null.</param>
        /// <returns>The newly created invoice ID.</returns>
        /// <exception cref="CustomerNotFoundException"></exception>
        Task<int> Create(int customerID, InvoiceModel invoice);

        /// <summary>
        /// Retrieve all invoices for a given customer.
        /// </summary>
        /// <param name="customerID">A valid customer ID.</param>
        /// <returns>List of invoices.</returns>
        /// <exception cref="CustomerNotFoundException"></exception>
        Task<IEnumerable<InvoiceModel>> All(int customerID);

        /// <summary>
        /// Retrieve a single invoice by its ID.
        /// </summary>
        /// <param name="invoiceID">A valid invoice ID.</param>
        /// <returns>The invoice.</returns>
        /// <exception cref="InvoiceNotFoundException"></exception>
        Task<InvoiceModel> Find(int invoiceID);

        /// <summary>
        /// Export an invoice as PDF.
        /// </summary>
        /// <param name="invoiceID">A valid invoice ID.</param>
        /// <returns>An URL to download the file.</returns>
        /// <exception cref="InvoiceNotFoundException"></exception>
        Task<string> Export(int invoiceID);

        /// <summary>
        /// Search for an invoice.
        /// </summary>
        /// <param name="invoiceSearchArgs">Arguments for the invoice search.</param>
        /// <returns>List of invoices.</returns>
        Task<IEnumerable<InvoiceModel>> Search(InvoiceSearchArgs invoiceSearchArgs);
    }
}