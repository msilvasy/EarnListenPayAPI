using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ListenPay.Data.Entities;
using ListenPay.WebApi.ResponseModel;
using ListenPay.WebApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ListenPay.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("{email}")]
        public async Task<ResponseModel<object>> Register([Required] string email)
        {
            try
            {        
                var user = await _userService.RegisterUser(email);
                return await Task.FromResult(new ResponseModel<object>(user));
            }
            catch (Exception ex)
            {
                return new ExceptionResponse<object>(ex.Message);
            }
        }

        [HttpPost("auth/{email}")]
        public async Task<ResponseModel<object>> Auth([Required] string email)
        {
            try
            {
                var user = await _userService.Login(email);
                if (user != null)
                {

                    return await Task.FromResult(new ResponseModel<object>(user));
                }
                else
                {
                    return await Task.FromResult(new NoResultFoundResponse<object>());
                }
            }
            catch (Exception ex)
            {
                return new ExceptionResponse<object>();
            }
        }

        [HttpPost("{email}/activity")]
        public async Task<ResponseModel<object>> Activity([Required] string email, Models.Activity activity)
        {
            try
            {
                var user = await _userService.AddActivity(email, activity);

                return await Task.FromResult(new ResponseModel<object>(user));
            }
            catch (Exception ex)
            {
                return new ExceptionResponse<object>();
            }
        }

        [HttpPost("{email}/teleportal")]
        public async Task<ResponseModel<object>> TelePortalAccount(string email, [Required] Models.TelePortalAccountModel telePortalAccountModel)
        {
            try
            {
                // default company id 1, all accounts created from listenPay will have association with copmany Id=1
                var telePortalAccount = await _userService.AddTelePortalAccountFromListenPay(email, telePortalAccountModel,1);

                return await Task.FromResult(new ResponseModel<object>(telePortalAccount));
            }
            catch (Exception ex)
            {
                return new ExceptionResponse<object>(ex.Message);
            }
        }

        [HttpGet("{email}/{pwd}/existing")]
        public async Task<ResponseModel<object>> AuthenticateExistingPortalAccount(string email, string pwd)
        {
            try
            {
                var authenticatedPortal = await _userService.GetByEmailPwd(email, pwd);

                return await Task.FromResult(new ResponseModel<object>(authenticatedPortal));
            }
            catch (Exception ex)
            {
                return new ExceptionResponse<object>();
            }
        }
    }
}