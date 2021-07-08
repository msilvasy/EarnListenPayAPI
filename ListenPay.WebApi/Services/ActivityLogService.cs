using ListenPay.Data;
using ListenPay.Data.Entities;
using ListenPay.WebApi.BindingModels;
using ListenPay.WebApi.Helpers;
using ListenPay.WebApi.IService;
using ListenPay.WebApi.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace ListenPay.WebApi.Services
{
    public class ActivityLogService : IActivityLog
    {

        private readonly ListenPayDbContext _context;

        public ActivityLogService(ListenPayDbContext context)
        {
            _context = context;
        }

        public async Task<ActivityLogModel> GetActiveLogDataSetByCompanyId(int companyId)
        {
            var activityLogModel = new ActivityLogModel();
            if (_context != null)
            {
                var resultByMonths = await (
                from activity in _context.ActivityLog
                where activity.CompanyId == companyId && activity.ActivityType == Constants.YOUTUBE_ACTIVITY_TYPE && activity.DateEntryModified.Value.Year == DateTime.Now.Year && activity.User.Active
                && (activity.DateEntryCreated == _context.ActivityLog.Where(p => p.UserId == activity.UserId).OrderByDescending(l => l.DateEntryCreated).Select(m => m.DateEntryCreated).FirstOrDefault())
                group activity by new { Month = activity.DateEntryModified.Value.Month } into a
                select new ActivityLogDataSet
                {
                    Earned = Math.Round(a.Sum(v => v.CurrentEarned), 2),
                    Duration = Math.Round(a.Sum(v => v.Duration), 2),
                    MonthId = a.Key.Month
                }).ToListAsync();

                var dataSetByMonths = new List<ActivityLogDataSet>();
                for (int i = 1; i <= 12; i++)
                {
                    var monthInfo = resultByMonths.FirstOrDefault(x => x.MonthId == i);
                    if (monthInfo != null)
                    {
                        monthInfo.Label = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i);
                        dataSetByMonths.Add(monthInfo);
                    }
                    else
                    {
                        dataSetByMonths.Add(new ActivityLogDataSet(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i), 0, 0));
                    }
                }
                var resultByDays = await (
         from activity in _context.ActivityLog
         where activity.CompanyId == companyId && activity.ActivityType == Constants.YOUTUBE_ACTIVITY_TYPE && activity.DateEntryModified.Value.Month == DateTime.Now.Month && activity.User.Active
         && (activity.DateEntryCreated == _context.ActivityLog.Where(p => p.UserId == activity.UserId).OrderByDescending(l => l.DateEntryCreated).Select(m => m.DateEntryCreated).FirstOrDefault())
         group activity by new { Day = activity.DateEntryModified.Value.Day, Month = activity.DateEntryModified.Value.Month } into a
         select new ActivityLogDataSet
         {
             Earned = Math.Round(a.Sum(v => v.CurrentEarned), 2),
             Duration = Math.Round(a.Sum(v => v.Duration), 2),
             DayId = a.Key.Day,
             MonthId = a.Key.Month,
             Label = ((DayOfWeek)((a.Key.Day + 1) % 7)).ToString(),
             MonthLabel = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(a.Key.Month)
         }).ToListAsync();

                var dataSetByDays = new List<ActivityLogDataSet>();
                var daysInMonth = System.DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
                for (int i = 1; i <= daysInMonth; i++)
                {
                    var monthInfo = resultByDays.FirstOrDefault(x => x.DayId == i);
                    if (monthInfo != null)
                    {
                        dataSetByDays.Add(monthInfo);
                    }
                    else
                    {
                        var tempDay = new ActivityLogDataSet(((DayOfWeek)((i + 1) % 7)).ToString(), 0, 0);
                        tempDay.DayId = i;
                        tempDay.MonthLabel = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.Now.Month);
                        tempDay.MonthId = DateTime.Now.Month;
                        dataSetByDays.Add(tempDay);
                    }
                }
                activityLogModel.ByDays = dataSetByDays;
                activityLogModel.ByMonths = dataSetByMonths;
                activityLogModel.Year = DateTime.Now.Year;
                activityLogModel.Month = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.Now.Month);

            }
            return activityLogModel;
        }
        public async Task<bool> AddUserVideoWatchTime(AddUserVideoWatchTimeBindingModel model)
        {
            if (_context != null)
            {
                var tempVideoActivity = await _context.UserVideoWatchActivity.FirstOrDefaultAsync(x => x.YouTubeVideoId == model.YouTubeVideoId && x.UserInformationId == model.UserId);
                if (tempVideoActivity == null)
                {
                    var youtube = await _context.YouTubeVideo.FirstOrDefaultAsync(x => x.YouTubeVideoId == model.YouTubeVideoId);
                    youtube.DurationInSeconds = model.VideoDuration;
                    _context.YouTubeVideo.Update(youtube);
                    await _context.SaveChangesAsync();
                    var watchActivity = new UserVideoWatchActivity()
                    {
                        UserInformationId = model.UserId,
                        YouTubeVideoId = model.YouTubeVideoId,
                        DateEntryCreated = DateTime.Now,
                        DateEntryModified = DateTime.Now,
                        WatchTimeInSeconds = model.WatchTimeInSecond,
                        CompanyId = model.CompanyId
                    };
                    await _context.UserVideoWatchActivity.AddAsync(watchActivity);
                    await _context.SaveChangesAsync();
                    return true;
                }
                if (model.WatchTimeInSecond > tempVideoActivity.WatchTimeInSeconds)
                {
                    tempVideoActivity.WatchTimeInSeconds = model.WatchTimeInSecond;
                    _context.Update(tempVideoActivity);
                    await _context.SaveChangesAsync();
                }
                return true;
            }
            return false;
        }

        public async Task<List<UserVideoWatchTimeModel>> GetUserTotalWatchTime(int companyId)
        {
            var userVideoWatchTimeList = new List<UserVideoWatchTimeModel>();
            if (_context != null)
            {

                double totalWatchTime = _context.YouTubeVideo.Where(x => x.CompanyId == companyId).Sum(x => x.DurationInSeconds);

                userVideoWatchTimeList = await (
                from activity in _context.UserVideoWatchActivity
                where activity.CompanyId == companyId
                group activity by new { UserId = activity.UserInformationId, UserName = activity.UserInformation.FirstName + " " + activity.UserInformation.LastName } into a
                select new UserVideoWatchTimeModel
                {
                    TotalWatchTimePercentage = Math.Round(((a.Sum(v => v.WatchTimeInSeconds) / totalWatchTime) * 100), 2),
                    UserId = a.Key.UserId,
                    UserName = a.Key.UserName,
                    From = (DateTime)a.Min(x => x.DateEntryCreated),
                    To = (DateTime)a.Max(x => x.DateEntryCreated)
                }).ToListAsync();

                var allUser = _context.TelePortalAccount.Where(x => x.CompanyId == companyId).Select(x => x.UserInformation).ToList().Select(x =>
                {
                    return new UserVideoWatchTimeModel { TotalWatchTimePercentage = 0, UserId = x.UserInformationId, UserName = x.FirstName + " " + x.LastName, From = DateTime.Now, To = DateTime.Now };
                }).ToList();
                allUser = allUser.Where(x => !userVideoWatchTimeList.Any(y => y.UserId == x.UserId)).ToList();
                userVideoWatchTimeList.AddRange(allUser);
            }
            return userVideoWatchTimeList.OrderByDescending(x => x.TotalWatchTimePercentage).ToList();
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

        ~ActivityLogService()
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

