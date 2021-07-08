using ListenPay.Data;
using ListenPay.Data.Entities;
using ListenPay.WebApi.Helpers;
using ListenPay.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace ListenPay.WebApi.Services
{
    public class UserService : IUserService
    {
        private readonly ListenPayDbContext _context;
        private readonly IKeyGeneratorService _keyGeneratorService;
        private readonly JwtSettings _jwtSettings;

        public UserService(ListenPayDbContext context, IKeyGeneratorService keyGeneratorService, IOptions<JwtSettings> jwtSettings)
        {
            _context = context;
            _keyGeneratorService = keyGeneratorService;
            _jwtSettings = jwtSettings.Value;
        }
        public async Task<UserInformation> GetByEmail(string email)
        {
            return await _context.Set<UserInformation>().SingleOrDefaultAsync(u => u.Email == email && u.Active);
        }

        public async Task<TelePortalAccountModel> GetByEmailPwd(string email, string pwd)
        {
            var telePortalAccount = await _context.Set<TelePortalAccount>().SingleOrDefaultAsync(u => u.CurrentPassword == pwd && u.UserInformation.Email == email && u.Active);
            if (telePortalAccount == null)
            {
                throw new Exception("Email or password is not correct");
            }
            var userInfo = await _context.Set<UserInformation>().SingleAsync(u => u.Email == email && u.Active);
            if (userInfo == null)
            {
                throw new Exception("Email or password is not correct");
            }

            // auto mapper

            var model = new TelePortalAccountModel
            {
                UserId = userInfo.UserInformationId,
                FullName = telePortalAccount.FullName,
                StreetAddress = telePortalAccount.StreetAddress,
                Unit = telePortalAccount.Unit,
                City = telePortalAccount.City,
                State = telePortalAccount.State,
                Zip = telePortalAccount.Zip,
                Phone = telePortalAccount.Phone,
                CurrentPassword = telePortalAccount.CurrentPassword,
                InsuranceCarrier = telePortalAccount.InsuranceCarrier,
                MemberId = telePortalAccount.MemberId,
                GroupNumber = telePortalAccount.GroupNumber,
                HashKey = userInfo.Key

            };

            return model;
        }

        public async Task<UserInformation> Login(string email)
        {
            var existingUser = await GetByEmail(email);
            var tempCompany = _context.TelePortalAccount.FirstOrDefault(x => x.UserInformation.UserInformationId == existingUser.UserInformationId);
            if (tempCompany == null)
            {
                //account is created from listenPay but portal account is not crated yet.
                existingUser.Token = Helper.GenerateJwtToken(1, _jwtSettings.Secret);
                existingUser.CompanyId = 1;
            }
            else
            {
                existingUser.Token = Helper.GenerateJwtToken(tempCompany.CompanyId, _jwtSettings.Secret);
                existingUser.CompanyId = tempCompany.CompanyId;
            }

            if (existingUser != null)
            {
                return existingUser;
            }
            else
            {
                return null;
            }
        }

        public async Task<UserInformation> RegisterUser(string email)
        {
            if (await GetByEmail(email) != null)
            {
                throw new ApplicationException("Email already registered!");
            }

            var newUser = new UserInformation
            {
                Email = email,
                Key = this._keyGeneratorService.GetBase36(128),
                Earned = 20,
                Discount = 30,
                DateEntryCreated = DateTime.Now,
                DateEntryModified = DateTime.Now,
                Active = true
            };
            _context.Set<UserInformation>().Add(newUser);
            await _context.SaveChangesAsync();

            return newUser;
        }

        public async Task<UserInformation> AddActivity(string email, Activity activity)
        {
            var user = await GetByEmail(email);
            if (user == null)
            {
                throw new Exception("User not found");
            }

            var activityLog = new ActivityLog
            {
                ActivityType = activity.ActivityType,
                Duration = activity.Duration,
                TimeStamp = DateTime.Now,
                TrackId = activity.TrackId,
                TrackName = activity.TrackName,
                NextMediaPlayed = activity.NextMediaPlayed,
                DateEntryCreated = DateTime.Now,
                DateEntryModified = DateTime.Now,
                Earned = (decimal)Math.Round(activity.Duration * 0.1, 2),
                OldEarned = user.Earned ?? 0,
                UserCreated = user.UserInformationId,
                UserModified = user.UserInformationId,
                UserId = user.UserInformationId,
                CompanyId = activity.CompanyId
            };

            activityLog.CurrentEarned = activityLog.Earned + activityLog.OldEarned;
            user.Earned = activityLog.CurrentEarned;
            user.UserModified = user.UserInformationId;
            user.DateEntryModified = DateTime.Now;
            var tempCompany = _context.TelePortalAccount.FirstOrDefault(x => x.UserInformation.UserInformationId == user.UserInformationId);
            user.Token = Helper.GenerateJwtToken(tempCompany.CompanyId, _jwtSettings.Secret);
            user.CompanyId = tempCompany.CompanyId;
            _context.Set<ActivityLog>().Add(activityLog);

            await _context.SaveChangesAsync();
            if (activity.ActivityType.ToLower().Trim() == Constants.YOUTUBE_ACTIVITY_TYPE.ToLower().Trim())
            {
                var userRelatedData = _context.Set<UserRelatedVideoData>().FirstOrDefault(x => x.UserInformationId == activity.UserInformationId && x.YouTubeVideoId == activity.YouTubeVideoId && x.DateEntryModified.Value.Day == DateTime.Now.Day && x.DateEntryModified.Value.Month == DateTime.Now.Month && x.DateEntryModified.Value.Year == DateTime.Now.Year);
                if (userRelatedData == null)
                {
                    var tempUserRelatedData = new UserRelatedVideoData()
                    {
                        CompanyId = activity.CompanyId,
                        UserInformationId = activity.UserInformationId,
                        Duration = activity.Duration,
                        YouTubeVideoCategoryId = activity.CategoryId,
                        YouTubeVideoId = activity.YouTubeVideoId,
                        DateEntryCreated = DateTime.Now,
                        DateEntryModified = DateTime.Now
                    };
                    await _context.Set<UserRelatedVideoData>().AddAsync(tempUserRelatedData);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    userRelatedData.Duration = activity.Duration;
                    userRelatedData.DateEntryModified = DateTime.Now;
                    _context.Set<UserRelatedVideoData>().Update(userRelatedData);
                    await _context.SaveChangesAsync();
                }
            }
            return user;
        }

        public async Task<TelePortalAccount> AddTelePortalAccountFromListenPay(string email, Models.TelePortalAccountModel telePortalAccountModel, int companyId)
        {
            var user = await GetByEmail(email);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            var account = _context.TelePortalAccount.FirstOrDefault(x => x.Email == email);
            if (account != null)
            {
                throw new Exception("Account already exist");
            }
            var telePortalAccount = new TelePortalAccount
            {
                FullName = telePortalAccountModel.FullName,
                StreetAddress = telePortalAccountModel.StreetAddress,
                Unit = telePortalAccountModel.Unit,
                City = telePortalAccountModel.City,
                State = telePortalAccountModel.State,
                Zip = telePortalAccountModel.Zip,
                Phone = telePortalAccountModel.Phone,
                CurrentPassword = telePortalAccountModel.CurrentPassword,
                InsuranceCarrier = telePortalAccountModel.InsuranceCarrier,
                MemberId = telePortalAccountModel.MemberId,
                GroupNumber = telePortalAccountModel.GroupNumber,
                UserInformation = user,
                CompanyId = companyId,
                Active = true
            };


            _context.Set<TelePortalAccount>().Add(telePortalAccount);

            await _context.SaveChangesAsync();

            return telePortalAccount;
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

        ~UserService()
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
