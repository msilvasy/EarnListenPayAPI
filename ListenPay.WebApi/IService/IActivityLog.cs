using ListenPay.WebApi.BindingModels;
using ListenPay.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListenPay.WebApi.IService
{
    public interface IActivityLog
    {
        Task<ActivityLogModel> GetActiveLogDataSetByCompanyId(int userInformationId);
        Task<bool> AddUserVideoWatchTime(AddUserVideoWatchTimeBindingModel model);
        Task<List<UserVideoWatchTimeModel>> GetUserTotalWatchTime(int companyId);
    }
}
