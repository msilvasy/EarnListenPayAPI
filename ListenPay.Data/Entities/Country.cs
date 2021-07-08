using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ListenPay.Data.Entities
{
    public class Country : BaseEntity
    {
        [Key]
        public int CountryId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }
}