using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListenPay.WebApi.BindingModels
{
    public class GetCategoryMatricsByBindingModel
    {
        public int CategoryId { get; set; }
        public int CompanyId { get; set; }
    }
}
