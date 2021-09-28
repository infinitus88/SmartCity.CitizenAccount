using AutoMapper;
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
        private readonly IMapper _mapper;

        public PaymentService(IGenericRepository repository, ICitizensService service, IMapper mapper)
        {
            _repository = repository;
            _service = service;
            _mapper = mapper;
        }

        private IQueryable<Invoice> GetQuery()
        {
            var query = _repository.Query<Invoice>();

            return query;
        }

        public IQueryable<Invoice> Get()
        {
            return GetQuery();
        }

        public IQueryable<Invoice> GetByCitizenId(string id)
        {
            var invoices = GetQuery().Where(p => p.CitizenId == id);

            return invoices;
        }

        public async Task<Invoice> CreateExpenseInvoice(MakePaymentModel model)
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
            var invoice = _mapper.Map<Invoice>(model);
            invoice.InvoiceType = InvoiceType.Expense;
            user.Balance -= model.Amount;
            _repository.Add(invoice);
            await _repository.SaveAsync();

            return invoice;
        }

        public async Task<Invoice> CreateGainInvoice(MakePaymentModel model)
        {
            var user = _service.Get(model.CitizenId);

            if (user == null)
            {
                throw new NotFoundException("User is not found");
            }

            var invoice = _mapper.Map<Invoice>(model);
            invoice.InvoiceType = InvoiceType.Gain;
            user.Balance += model.Amount;
            _repository.Add(invoice);
            await _repository.SaveAsync();

            return invoice;
        }
    }
}
