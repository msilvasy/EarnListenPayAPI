using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ListenPay.WebApi.BindingModels
{
    public class AddYouTubeCategoryBindingModel
    {
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
