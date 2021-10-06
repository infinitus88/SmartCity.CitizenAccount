using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartCity.CitizenAccount.Api.Models.Payment;
using SmartCity.CitizenAccount.Data.Access.Constants;
using SmartCity.CitizenAccount.Data.Models;
using SmartCity.CitizenAccount.Security;
using SmartCity.CitizenAccount.Services.PaymentAppService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartCity.CitizenAccount.Server.RestAPI
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _service;
        private readonly IMapper _mapper;

        public PaymentController(IPaymentService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet("invoices")]
        [Authorize]
        public IQueryable<InvoiceModel> GetInvoices()
        {
            var invoices = _service.GetRelatedInvoices();

            return invoices.ProjectTo<InvoiceModel>(_mapper.ConfigurationProvider);
        }

        [HttpGet("invoices/{id}")]
        public InvoiceDetailModel GetInvoiceDetail(int id)
        {
            var invoice = _service.GetById(id);

            return _mapper.Map<InvoiceDetailModel>(invoice);
        }

        [HttpPost("make-payment")]
        public async Task<PaymentResultModel> MakePayment([FromBody] MakePaymentModel model)
        {
            var invoice = await _service.CreateExpenseInvoice(model);

            return new PaymentResultModel { Amount = invoice.Amount, IsSucceed = true };
        }

        [HttpPost("give-benefits")]
        [Authorize(Roles = Roles.Administrator)]
        public async Task<PaymentResultModel> GiveBenefits([FromBody] GiveBenefitsModel model)
        {
            foreach (var citizenId in model.CitizenIds)
            {
                var makePaymentModel = new MakePaymentModel { Category = "benefits", CitizenId = citizenId, Amount = model.Amount, ServiceId = 1 };
                await _service.CreateGainInvoice(makePaymentModel);
            }

            return new PaymentResultModel { Amount = model.Amount, IsSucceed = true };
        }

        [HttpGet("services")]
        public IQueryable<ServiceDetailModel> GetServices()
        {
            var service = _service.GetServices();

            return service.ProjectTo<ServiceDetailModel>(_mapper.ConfigurationProvider);
        }

        [HttpGet("services/{id}")]
        public ServiceDetailModel GetServiceDetail(int id)
        {
            var service = _service.GetServiceById(id);

            return _mapper.Map<ServiceDetailModel>(service);
        }


        [HttpPost("add-service")]
        public async Task<ServiceDetailModel> AddService([FromBody] CreateServiceModel model)
        {
            var service = await _service.CreateService(model);

            return _mapper.Map<ServiceDetailModel>(service);
        }

        [HttpPost("update-service/{id}")]
        public async Task<ServiceDetailModel> UpdateService(int id, [FromBody] UpdateServiceModel model)
        {
            var service = await _service.UpdateService(id, model);

            return _mapper.Map<ServiceDetailModel>(service);
        }

        [HttpDelete("delete-service/{id}")]
        public async Task<IActionResult> DeleteService(int id)
        {
            await _service.DeleteService(id);

            return Ok();
        }
    }
}
