using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ListenPay.WebApi.BindingModels;
using ListenPay.WebApi.IService;
using ListenPay.WebApi.ResponseModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ListenPay.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivityLogController : ControllerBase
    {

        private readonly IActivityLog _activityLogService;

        public ActivityLogController(IActivityLog activityLog)
        {
            _activityLogService = activityLog;
        }
        [HttpGet()]
        [Authorize]
        [Route("GetActiveLogDataSetByCompanyId")]
        public async Task<ResponseModel<object>> GetActiveLogDataSetByCompanyId([Required] int companyId)
        {
            try
            {
                var result = await _activityLogService.GetActiveLogDataSetByCompanyId(companyId);
                return await Task.FromResult(new ResponseModel<object>(result));
            }
            catch (Exception ex)
            {
                return new ExceptionResponse<object>();
            }
        }
        [HttpPost()]
        [Authorize]
        [Route("AddUserVideoWatchTime")]
        public async Task<ResponseModel<object>> AddUserVideoWatchTime([Required] AddUserVideoWatchTimeBindingModel model)
        {
            try
            {
                var result = await _activityLogService.AddUserVideoWatchTime(model);
                return await Task.FromResult(new ResponseModel<object>(result));
            }
            catch (Exception ex)
            {
                return new ExceptionResponse<object>();
            }
        }

        [HttpGet()]
        [Authorize]
        [Route("GetUserTotalWatchTime")]
        public async Task<ResponseModel<object>> GetUserTotalWatchTime([Required] int companyId)
        {
            try
            {
                var result = await _activityLogService.GetUserTotalWatchTime(companyId);
                return await Task.FromResult(new ResponseModel<object>(result));
            }
            catch (Exception ex)
            {
                return new ExceptionResponse<object>();
            }
        }
    }
}
