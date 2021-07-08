using System;
using System.Collections.Generic;
using System.Text;

namespace ListenPay.Data.Entities
{
    public class CategoryMatrics : BaseEntity
    {
        public int CategoryMatricsId { get; set; }
        public int YouTubeVideoCategoryId { get; set; }
        public YouTubeVideoCategory YouTubeVideoCategory { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }
        public double TargetMean { get; set; }
        public double ReportedAccident { get; set; }
    }
}
