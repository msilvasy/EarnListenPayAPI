using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ListenPay.WebApi.BindingModels
{
    public class AddUserVideoWatchTimeBindingModel
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public int YouTubeVideoId { get; set; }
        [Required]
        public double WatchTimeInSecond { get; set; }
        [Required]
        public double VideoDuration { get; set; }
        [Required]
        public int CompanyId { get; set; }
    }
}
