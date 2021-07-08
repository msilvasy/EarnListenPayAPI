using System;
using System.Collections.Generic;
using System.Text;

namespace ListenPay.Data.Entities
{
    public class MediaPartner : BaseEntity
    {
        public int Id { get; set; }
        public string CompnayArtist { get; set; }
        public string URL { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Comments { get; set; }
        public string Products { get; set; }
        public virtual ICollection<MediaPartnerProduct> MediaPartnerProduct { get; set; }
    }
}
