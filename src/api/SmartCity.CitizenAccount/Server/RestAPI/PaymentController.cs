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

        [Route("transfer-amount")]
        public async Task<PaymentResultModel> TransferAmount([FromBody] MakePaymentModel model)
        {
            var invoice = await _service.CreateGainInvoice(model);

            return new PaymentResultModel { Amount = invoice.Amount, IsSucceed = true };
        }
    }
}
