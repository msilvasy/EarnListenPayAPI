using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListenPay.WebApi.Models
{
    public class ActivityLogModel
    {
        public int Year { get; set; }
        public string Month { get; set; }
        public List<ActivityLogDataSet> ByMonths { get; set; }
        public List<ActivityLogDataSet> ByDays { get; set; }
    }

    public class ActivityLogDataSet
    {
        public ActivityLogDataSet()
        { }
        public ActivityLogDataSet(string label, Decimal earned, double duration)
        {
            Label = label;
            Earned = earned;
            Duration = duration;
        }
        public string Label { get; set; }
        public Decimal Earned { get; set; }
        public double Duration { get; set; }
        public int MonthId { get; set; }
        public int DayId { get; set; }
        public string MonthLabel { get; set; }
    }
}
