using System;
using System.Collections.Generic;
using System.Text;

namespace ListenPay.Data.Entities
{
    public class MediaPartnerProduct : BaseEntity
    {
        public string ProductTitle { get; set; }
        public int Id { get; set; }
        public int MediaPartnerId { get; set; }
        public MediaPartner MediaPartner { get; set; }
    }
}
