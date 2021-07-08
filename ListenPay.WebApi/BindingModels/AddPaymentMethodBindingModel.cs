using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ListenPay.WebApi.BindingModels
{
    public class AddPaymentMethodBindingModel
    {
        [Required]
        public string PaymentMethodTypeTitle { get; set; }
        public int UserInformationId { get; set; }
    }
}
