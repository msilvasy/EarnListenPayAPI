using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListenPay.WebApi.BindingModels
{
    public class AddCompanyRelatedYouTubeCategory
    {
        public string Title { get; set; }
        public int CompanyId { get; set; }
        public string Description { get; set; }
    }
}
