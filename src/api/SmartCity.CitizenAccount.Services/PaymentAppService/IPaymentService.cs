﻿using SmartCity.CitizenAccount.Api.Models.Payment;
using SmartCity.CitizenAccount.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCity.CitizenAccount.Services.PaymentAppService
{
    public interface IPaymentService
    {
        IQueryable<PaymentBill> Get();
        IQueryable<PaymentBill> GetByCitizenId(string id);
        Task<PaymentBill> Create(MakePaymentModel model);
    }
}
