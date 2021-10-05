using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SmartCity.CitizenAccount.Api.Common.Exceptions;
using SmartCity.CitizenAccount.Api.Models.Payment;
using SmartCity.CitizenAccount.Data.Access.DAL.Repositories;
using SmartCity.CitizenAccount.Data.Models;
using SmartCity.CitizenAccount.Security;
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
        private readonly ISecurityContext _context;

        public PaymentService(IGenericRepository repository, ICitizensService service, IMapper mapper, ISecurityContext context)
        {
            _repository = repository;
            _service = service;
            _mapper = mapper;
            _context = context;
        }

        private IQueryable<Invoice> GetQuery()
        {
            var query = _repository.Query<Invoice>();

            return query;
        }

        public IQueryable<Invoice> Get()
        {
            return GetQuery().Include(i => i.Service);
        }

        public Invoice GetById(int id)
        {
            var invoice = GetQuery().Where(i => i.Id == id)
                .Include(i => i.Citizen)
                .Include(i => i.Service).FirstOrDefault();

            if (invoice == null)
            {
                throw new NotFoundException("Invoice is not found");
            }

            return invoice;
        }

        public IQueryable<Invoice> GetRelatedInvoices()
        {
            var invoices = GetQuery()
                .Where(p => p.CitizenId == _context.User.CitizenId)
                .Include(i => i.Service);

            return invoices;
        }

        public async Task<Invoice> CreateExpenseInvoice(MakePaymentModel model)
        {
            var service = GetServicesQuery()
                .Where(s => s.Id == model.ServiceId).FirstOrDefault();
            
            if (service == null)
            {
                throw new NotFoundException("Service is not found");
            }

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
            var service = GetServicesQuery()
                .Where(s => s.Id == model.ServiceId).FirstOrDefault();

            if (service == null)
            {
                throw new NotFoundException("Service is not found");
            }

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

        private IQueryable<Service> GetServicesQuery()
        {
            var query = _repository.Query<Service>().Where(s => !s.IsDeleted);

            return query;
        }

        public IQueryable<Service> GetServices()
        {
            return GetServicesQuery();
        }

        public async Task<Service> CreateService(CreateServiceModel model)
        {
            var service = _mapper.Map<Service>(model);

            _repository.Add(service);
            await _repository.SaveAsync();

            return service;
        }

        public async Task<Service> UpdateService(int id, UpdateServiceModel model)
        {
            var service = GetServices().Where(s => s.Id == id).FirstOrDefault();

            if (service == null)
            {
                throw new NotFoundException("Service is not found");
            }

            _mapper.Map(model, service);
            await _repository.SaveAsync();

            return service;
        }

        public async Task DeleteService(int id)
        {
            var service = GetServices().Where(s => s.Id == id).FirstOrDefault();

            if (service == null)
            {
                throw new NotFoundException("Service is not found");
            }

            if (service.IsDeleted) return;

            service.IsDeleted = true;
            await _repository.SaveAsync();
        }

        public Service GetServiceById(int id)
        {
            var service = GetServicesQuery().Where(s => s.Id == id).FirstOrDefault();

            if (service == null)
            {
                throw new NotFoundException("Service is not found");
            }

            return service;
        }
    }
}
