using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ListenPay.WebApi.BindingModels
{
    public class GetYouTubeVideosByCompanyAndUserIdBindingModel
    {
        [Required]
        public int CompanyId { get; set; }
        [Required]
        public int UserId { get; set; }
    }
}
