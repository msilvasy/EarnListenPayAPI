using System;
using System.Collections.Generic;
using System.Text;

namespace ListenPay.Data.Entities
{
    public class ActivityLog : BaseEntity
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public virtual UserInformation User { get; set; }
        public string TrackId { get; set; }

        public string TrackName { get; set; }

        public double Duration { get; set; }

        public DateTime TimeStamp { get; set; }

        public string ActivityType { get; set; }

        public decimal Earned { get; set; }

        public decimal OldEarned { get; set; }

        public decimal CurrentEarned { get; set; }

        public string NextMediaPlayed { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }
    }
}
