using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SmartCity.CitizenAccount.Api.Models.Payment;
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

        [Route("proceed-payment")]
        public async Task<PaymentResultModel> MakePayment([FromBody] MakePaymentModel model)
        {
            var invoice = await _service.CreateExpenseInvoice(model);

            return new PaymentResultModel { Amount = invoice.Amount, IsSucceed = true };
        }

        [Route("give-benefits")]
        public async Task<PaymentResultModel> GiveBenefits([FromBody] GiveBenefitsModel model)
        {
            foreach (var citizenId in model.CitizenIds)
            {
                var makePaymentModel = new MakePaymentModel { CitizenId = citizenId, Amount = model.Amount, ServiceName = model.ServiceName };
                await _service.CreateGainInvoice(makePaymentModel);
            }

            return new PaymentResultModel { Amount = model.Amount, IsSucceed = true };
        }
    }
}
