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
using WebApi.Models;

namespace ListenPay.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private ICompany _companyService;

        public CompanyController(ICompany userService)
        {
            _companyService = userService;
        }

        [HttpPost("GetLogin")]
        public async Task<ResponseModel<object>> GetLogin(AuthenticateRequestBindingModel model)
        {
            try
            {
                var response = _companyService.Authenticate(model);
                if (response == null)
                {
                    return await Task.FromResult(new ResponseModel<object>("Username or password is incorrect", ResponseCode.Error));
                }
                return await Task.FromResult(new ResponseModel<object>(response));
            }
            catch (Exception ex)
            {
                return new ExceptionResponse<object>();
            }
        }
        [HttpPost()]
        [Authorize]
        [Route("AddCompany")]
        public async Task<ResponseModel<object>> AddCompany([FromBody] AddCompanyBindingModel model)
        {
            try
            {
                if (_companyService.GetCompanyByEmail(model.Email) != null)
                {
                    return await Task.FromResult(new ResponseModel<object>("Email already registered!", ResponseCode.Error));
                }
                var result = await _companyService.AddCompany(model);
                return await Task.FromResult(new ResponseModel<object>(result));
            }
            catch (Exception ex)
            {
                return new ExceptionResponse<object>();
            }
        }

        [HttpPost()]
        [Authorize]
        [Route("AddUpdateCompanyUserTelePortalAccount")]
        public async Task<ResponseModel<object>> AddUpdateCompanyUserTelePortalAccount([FromBody] AddCompanyUserTelePortalBindingModel model)
        {
            try
            {
                var result = await _companyService.AddUpdateCompanyUserTelePortalAccount(model);
                if (result == null)
                {
                    return await Task.FromResult(new ResponseModel<object>("Something went wrong!", ResponseCode.Error));
                }
                return await Task.FromResult(new ResponseModel<object>(true));
            }
            catch (Exception ex)
            {
                return new ExceptionResponse<object>();
            }
        }
        [HttpGet()]
        [Authorize]
        [Route("GetCompanyUserTelePortalAccount")]
        public async Task<ResponseModel<object>> GetCompanyUserTelePortalAccount([Required] int companyId)
        {
            try
            {
                var result = await _companyService.GetCompanyUserTelePortalAccount(companyId);
                if (result == null)
                {
                    return await Task.FromResult(new ResponseModel<object>("Something went wrong!", ResponseCode.Error));
                }
                return await Task.FromResult(new ResponseModel<object>(result));
            }
            catch (Exception ex)
            {
                return new ExceptionResponse<object>();
            }
        }
        [HttpPost()]
        [Authorize]
        [Route("DeleteCompanyUserTelePortalAccount")]
        public async Task<ResponseModel<object>> DeleteCompanyUserTelePortalAccount([FromBody] DeleteCompanyUserTelePortalBindingModel model)
        {
            try
            {
                var result = await _companyService.DeleteCompanyUserTelePortalAccount(model.UserId);
                return await Task.FromResult(new ResponseModel<object>(result));
            }
            catch (Exception ex)
            {
                return new ExceptionResponse<object>();
            }
        }
    }
}
