using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListenPay.WebApi.BindingModels
{
    public class AddMediaPartnerBindingModel
    {
        public string CompanyArtist { get; set; }
        public string WebSiteURL { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Comments { get; set; }
        public string SelectProducts { get; set; }
    }
}
