using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListenPay.WebApi.Models
{
    public class UserVideoWatchTimeModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public double TotalWatchTimePercentage { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
    }
}
