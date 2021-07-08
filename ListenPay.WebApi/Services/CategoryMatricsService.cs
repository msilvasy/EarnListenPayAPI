using ListenPay.Data;
using ListenPay.Data.Entities;
using ListenPay.WebApi.IService;
using ListenPay.WebApi.Models;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace ListenPay.WebApi.Services
{
    public class CategoryMatricsService : ICategoryMatricsService
    {

        private readonly ListenPayDbContext _context;

        public CategoryMatricsService(ListenPayDbContext context)
        {
            _context = context;
        }
        public async Task<CategoryMatricsDataSetModel> GetCategoryMatricsById(int companyId, int categoryId)
        {
            var activityLogModel = new CategoryMatricsDataSetModel();
            if (_context != null)
            {
                var resultByMonths = await (
       from matrics in _context.CategoryMatrics
       where matrics.CompanyId == companyId && matrics.YouTubeVideoCategoryId == categoryId && matrics.DateEntryModified.Value.Year == DateTime.Now.Year
       group matrics by new { Month = matrics.DateEntryModified.Value.Month } into a
       select new CategoryMatricsData
       {
           ReportedAccident = Math.Round(a.Sum(v => v.ReportedAccident), 2),
           TargetMean = Math.Round(a.Sum(v => v.TargetMean), 2),
           MonthId = a.Key.Month
       }).ToListAsync();

                activityLogModel.CategoryTotalDuration = _context.YouTubeVideo.Include(y => y.YouTubeVideoRelatedCategories).Where(x => x.YouTubeVideoRelatedCategories.Any(p => (p.YouTubeVideoCategoryId == categoryId && x.CompanyId == companyId))).Sum(y => y.DurationInSeconds);


                activityLogModel.YearTargetMeanCount = _context.CategoryMatrics.Where(x => x.CompanyId == companyId && x.YouTubeVideoCategoryId == categoryId && x.DateEntryModified.Value.Year == DateTime.Now.Year).Count();
                var dataSetByMonths = new List<CategoryMatricsData>();
                for (int i = 1; i <= 12; i++)
                {

                    var noReportedAccident = _context.CategoryMatrics.Where(x => x.CompanyId == companyId && x.YouTubeVideoCategoryId == categoryId
                    && x.DateEntryModified.Value.Year == DateTime.Now.Year && x.DateEntryModified.Value.Month == DateTime.Now.Month).Count();

                    var userVideoWatchTimeList = await (
               from activity in _context.UserRelatedVideoData
               where activity.CompanyId == companyId && activity.YouTubeVideoCategoryId == categoryId && activity.DateEntryModified.Value.Year == DateTime.Now.Year && activity.DateEntryModified.Value.Month == DateTime.Now.Month
               group activity by new { UserId = activity.UserInformationId } into a
               select new UserVideoWatchTimeModel
               {
                   TotalWatchTimePercentage = Math.Round(((a.Max(v => v.Duration) / activityLogModel.CategoryTotalDuration) * 100), 2),
                   UserId = a.Key.UserId,
               }).ToListAsync();

                    var monthInfo = resultByMonths.FirstOrDefault(x => x.MonthId == i);
                    if (monthInfo != null)
                    {
                        var userWatchTime = userVideoWatchTimeList.Sum(x => x.TotalWatchTimePercentage);
                        monthInfo.Label = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i);
                        monthInfo.ActualMean = noReportedAccident > 0 ? monthInfo.TargetMean / noReportedAccident : 0;
                        monthInfo.CategoryWatchDurationInPercentage = userWatchTime > 0 ? Math.Round(((userWatchTime / userVideoWatchTimeList.Count())), 2) : 0;
                        dataSetByMonths.Add(monthInfo);
                    }
                    else
                    {
                        var tempMonthInfo = new CategoryMatricsData(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i), 0, 0);
                        tempMonthInfo.CategoryWatchDurationInPercentage = 0;
                        tempMonthInfo.ActualMean = 0;
                        dataSetByMonths.Add(tempMonthInfo);
                    }
                }

                var resultByDays = await (
        from matrics in _context.CategoryMatrics
        where matrics.CompanyId == companyId && matrics.YouTubeVideoCategoryId == categoryId && matrics.DateEntryModified.Value.Month == DateTime.Now.Month
        group matrics by new { Day = matrics.DateEntryModified.Value.Day, Month = matrics.DateEntryModified.Value.Month } into a
        select new CategoryMatricsData
        {
            ReportedAccident = Math.Round(a.Sum(v => v.ReportedAccident), 2),
            TargetMean = Math.Round(a.Sum(v => v.TargetMean), 2),
            DayId = a.Key.Day,
            MonthId = a.Key.Month,
            Label = ((DayOfWeek)((a.Key.Day + 1) % 7)).ToString(),
            MonthLabel = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(a.Key.Month)
        }).ToListAsync();

                activityLogModel.MonthTargetMeanCount = _context.CategoryMatrics.Where(x => x.CompanyId == companyId && x.YouTubeVideoCategoryId == categoryId && x.DateEntryModified.Value.Month == DateTime.Now.Month).Count();
                var dataSetByDays = new List<CategoryMatricsData>();

                var daysInMonth = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
                for (int i = 1; i <= daysInMonth; i++)
                {
                    var monthInfo = resultByDays.FirstOrDefault(x => x.DayId == i);
                    var noReportedAccident = _context.CategoryMatrics.Where(x => x.CompanyId == companyId && x.YouTubeVideoCategoryId == categoryId && x.DateEntryModified.Value.Day == i
                    && x.DateEntryModified.Value.Month == DateTime.Now.Month).Count();



                    var userVideoWatchTimeList = await (
                        from activity in _context.UserRelatedVideoData
                        where activity.CompanyId == companyId && activity.YouTubeVideoCategoryId == categoryId && activity.DateEntryModified.Value.Day == i && activity.DateEntryModified.Value.Month == DateTime.Now.Month
                        group activity by new { UserId = activity.UserInformationId } into a
                        select new UserVideoWatchTimeModel
                        {
                            TotalWatchTimePercentage = Math.Round(((a.Max(v => v.Duration) / activityLogModel.CategoryTotalDuration) * 100), 2),
                            UserId = a.Key.UserId,
                        }).ToListAsync();


                    if (monthInfo != null)
                    {
                        var userWatchTime = userVideoWatchTimeList.Sum(x => x.TotalWatchTimePercentage);
                        monthInfo.CategoryWatchDurationInPercentage = userWatchTime > 0 ? Math.Round(((userVideoWatchTimeList.Sum(x => x.TotalWatchTimePercentage) / userVideoWatchTimeList.Count())), 2) : 0;
                        monthInfo.ActualMean = noReportedAccident > 0 ? monthInfo.TargetMean / noReportedAccident : 0;
                        dataSetByDays.Add(monthInfo);
                    }
                    else
                    {
                        var tempDay = new CategoryMatricsData(((DayOfWeek)((i + 1) % 7)).ToString(), 0, 0);
                        tempDay.DayId = i;
                        tempDay.MonthLabel = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.Now.Month);
                        tempDay.MonthId = DateTime.Now.Month;
                        tempDay.CategoryWatchDurationInPercentage = 0;
                        tempDay.ActualMean = 0;
                        dataSetByDays.Add(tempDay);
                    }
                }
                activityLogModel.ByDays = dataSetByDays.ToList();
                activityLogModel.ByMonths = dataSetByMonths.ToList();
                activityLogModel.Year = DateTime.Now.Year;
                activityLogModel.Month = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.Now.Month);

            }
            return activityLogModel;
        }

        #region IDisposable Support
        private bool _disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                }
                _context.DisposeAsync();
                _disposedValue = true;
            }
        }

        ~CategoryMatricsService()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(false);
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
