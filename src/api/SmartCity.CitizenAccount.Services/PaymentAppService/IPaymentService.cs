using SmartCity.CitizenAccount.Api.Models.Payment;
using SmartCity.CitizenAccount.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCity.CitizenAccount.Services.PaymentAppService
{
    public interface IPaymentService
    {
        IQueryable<Invoice> Get();
        Invoice GetById(int id);
        IQueryable<Invoice> GetRelatedInvoices();
        Task<Invoice> CreateExpenseInvoice(MakePaymentModel model);
        Task<Invoice> CreateGainInvoice(MakePaymentModel model);

        IQueryable<Service> GetServices();
        Service GetServiceById(int id);
        Task<Service> CreateService(CreateServiceModel model);
        Task<Service> UpdateService(int id, UpdateServiceModel model);
        Task DeleteService(int id);
    }
}
