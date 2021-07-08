using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListenPay.WebApi.Models
{
    public class CategoryPoint
    {
        public int CategoryId { get; set; }
        public double Points { get; set; }
        public string Title { get; set; }
    }
}
