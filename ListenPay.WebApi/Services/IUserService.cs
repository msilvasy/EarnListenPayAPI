using ListenPay.Data.Entities;
using ListenPay.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListenPay.WebApi.Services
{
    public interface IUserService : IDisposable
    {
        Task<UserInformation> GetByEmail(string email);
        Task<UserInformation> RegisterUser(string email);
        Task<UserInformation> Login(string email);
        Task<UserInformation> AddActivity(string email, Activity activity);
        Task<TelePortalAccount> AddTelePortalAccountFromListenPay(string email, TelePortalAccountModel telePortalAccountModel, int companyId);

        Task<TelePortalAccountModel> GetByEmailPwd(string email, string pwd);
    }
}
