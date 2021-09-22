using SmartCity.CitizenAccount.Api.Common.Exceptions;
using SmartCity.CitizenAccount.Api.Models.Payment;
using SmartCity.CitizenAccount.Data.Access.DAL.Repositories;
using SmartCity.CitizenAccount.Data.Models;
using SmartCity.CitizenAccount.Services.CitizenAppService;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SmartCity.CitizenAccount.Services.PaymentAppService
{
    public class PaymentService : IPaymentService
    {
        private readonly IGenericRepository _repository;
        private readonly ICitizensService _service;

        public PaymentService(IGenericRepository repository, ICitizensService service)
        {
            _repository = repository;
            _service = service;
        }

        private IQueryable<PaymentBill> GetQuery()
        {
            var query = _repository.Query<PaymentBill>();

            return query;
        }

        public IQueryable<PaymentBill> Get()
        {
            return GetQuery();
        }

        public IQueryable<PaymentBill> GetByCitizenId(string id)
        {
            var paymetBills = GetQuery().Where(p => p.CitizenId == id);

            return paymetBills;
        }

        public async Task<PaymentBill> Create(MakePaymentModel model)
        {
            var user = _service.Get(model.CitizenId);

            if (user == null)
            {
                throw new NotFoundException("User is not found");
            }

            if (user.Balance >= model.Amount)
            {
                throw new BadRequestException("Not enough funds in the balance");
            }

            var paymentBill = new PaymentBill { CitizenId = model.CitizenId, Amount = model.Amount };
            user.Balance -= model.Amount;
            _repository.Add(paymentBill);
            await _repository.SaveAsync();

            return paymentBill;
        }
    }
}
