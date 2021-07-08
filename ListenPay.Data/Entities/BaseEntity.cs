using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListenPay.Data.Entities
{
    public abstract class BaseEntity
    {
        public DateTime? DateEntryCreated { get; set; }
        public int? UserCreated { get; set; }
        public DateTime? DateEntryModified { get; set; }
        public int? UserModified { get; set; }
    }
}