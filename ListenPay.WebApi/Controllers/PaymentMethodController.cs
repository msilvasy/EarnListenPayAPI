using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ListenPay.WebApi.BindingModels;
using ListenPay.WebApi.IService;
using ListenPay.WebApi.ResponseModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ListenPay.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentMethodController : ControllerBase
    {
        private IPaymentMethodService _paymentMethodService;

        public PaymentMethodController(IPaymentMethodService paymentMethodService)
        {
            _paymentMethodService = paymentMethodService;
        }
        [HttpPost()]
        [Route("AddPaymentMethod")]
        public async Task<ResponseModel<object>> AddPaymentMethod([FromBody] AddPaymentMethodBindingModel model)
        {
            try
            {
                var result = await _paymentMethodService.AddPaymentMethod(model.PaymentMethodTypeTitle, model.UserInformationId);
                return await Task.FromResult(new ResponseModel<object>(result));
            }
            catch (Exception ex)
            {
                return new ExceptionResponse<object>();
            }
        }
    }
}
