using MicroERP.Business.Domain.Models;
using System.Threading.Tasks;

namespace MicroERP.Business.Domain.Repositories
{
    public interface IInvoiceRepository
    {
        Task<InvoiceModel> Create(int customerID, InvoiceModel invoice);

        Task<InvoiceModel> Read(int customerID, int invoiceID);
    }
}
