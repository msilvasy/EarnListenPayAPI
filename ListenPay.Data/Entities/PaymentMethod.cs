using System;
using System.Collections.Generic;
using System.Text;

namespace ListenPay.Data.Entities
{
    public class PaymentMethod : BaseEntity
    {
        public int PaymentMethodId { get; set; }
        public string PaymentMethodTypeTitle { get; set; }
        public int UserInformationId { get; set; }

    }
}
