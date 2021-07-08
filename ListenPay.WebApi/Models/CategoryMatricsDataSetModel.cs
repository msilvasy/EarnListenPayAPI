using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListenPay.WebApi.Models
{
    public class CategoryMatricsDataSetModel
    {
        public int Year { get; set; }
        public string Month { get; set; }
        public int MonthTargetMeanCount { get; set; }
        public int YearTargetMeanCount { get; set; }
        public double CategoryTotalDuration { get; set; }
        public List<CategoryMatricsData> ByMonths { get; set; }
        public List<CategoryMatricsData> ByDays { get; set; }
    }

    public class CategoryMatricsData
    {
        public CategoryMatricsData()
        { }
        public CategoryMatricsData(string label, double reportedAccident, double targetMean)
        {
            Label = label;
            ReportedAccident = reportedAccident;
            TargetMean = targetMean;
        }
        public string Label { get; set; }
        public double ReportedAccident { get; set; }
        public double TargetMean { get; set; }
        public double ActualMean { get; set; }
        public double CategoryWatchDurationInPercentage { get; set; }
        public int MonthId { get; set; }
        public int DayId { get; set; }
        public string MonthLabel { get; set; }
    }
}
