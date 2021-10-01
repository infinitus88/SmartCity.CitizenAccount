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
        IQueryable<Invoice> GetByCitizenId(string id);
        Task<Invoice> CreateExpenseInvoice(MakePaymentModel model);
        Task<Invoice> CreateGainInvoice(MakePaymentModel model);
    }
}
