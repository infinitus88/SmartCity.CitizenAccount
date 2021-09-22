using SmartCity.CitizenAccount.Api.Models.Payment;
using SmartCity.CitizenAccount.Data.Access.DAL.Repositories;
using SmartCity.CitizenAccount.Data.Models;
using SmartCity.CitizenAccount.Services.CitizenAppService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartCity.CitizenAccount.Services.PaymentAppService
{
    public interface IPaymentService
    {
        IQueryable<PaymentBill> Get();
        IQueryable<PaymentBill> GetByCitizenId(string id);
        PaymentBill Create(MakePaymentModel model);
    }

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

        public PaymentBill Create(MakePaymentModel model)
        {
            throw new NotImplementedException();
        }
    }
}
