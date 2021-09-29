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
        private readonly ISecurityContext _context;
        private readonly IMapper _mapper;

        public PaymentController(IPaymentService service, IMapper mapper, ISecurityContext context)
        {
            _service = service;
            _mapper = mapper;
            _context = context;
        }

        [HttpGet("invoices")]
        [Authorize]
        public IQueryable<InvoiceModel> GetInvoices()
        {
            var invoices = _service.GetByCitizenId(_context.User.CitizenId);

            return invoices.ProjectTo<InvoiceModel>(_mapper.ConfigurationProvider);
        }

        [HttpPost("proceed-payment")]
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
                var makePaymentModel = new MakePaymentModel { Category = "benefits", CitizenId = citizenId, Amount = model.Amount, ServiceName = "City Administration" };
                await _service.CreateGainInvoice(makePaymentModel);
            }

            return new PaymentResultModel { Amount = model.Amount, IsSucceed = true };
        }
    }
}
