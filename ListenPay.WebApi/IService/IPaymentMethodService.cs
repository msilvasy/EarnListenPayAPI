using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListenPay.WebApi.IService
{
    public interface IPaymentMethodService
    {
        Task<bool> AddPaymentMethod(string paymentMethod, int userInformationId);
    }
}
