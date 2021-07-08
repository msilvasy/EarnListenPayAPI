using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ListenPay.WebApi.BindingModels;
using ListenPay.WebApi.IService;
using ListenPay.WebApi.Models;
using ListenPay.WebApi.ResponseModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ListenPay.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class YouTubeVideoController : ControllerBase
    {
        private readonly IYoutubeVideoService _youtubeVideoService;
        public YouTubeVideoController(IYoutubeVideoService youtubeService)
        {
            _youtubeVideoService = youtubeService;
        }
        [HttpPost()]
        [Authorize]
        [Route("AddYouTubeVideo")]
        public async Task<ResponseModel<object>> AddYouTubeVideo([FromBody] AddYouTubeVideoBindingModel model)
        {
            try
            {
                var result = await _youtubeVideoService.AddYoutubeVideo(model);
                return await Task.FromResult(new ResponseModel<object>(result));
            }
            catch (Exception ex)
            {
                return new ExceptionResponse<object>();
            }
        }

        [HttpGet()]
        [Authorize]
        [Route("GetYouTubeVideosByCompanyId")]
        public async Task<ResponseModel<object>> GetYouTubeVideosByCompanyId([Required] int companyId)
        {
            try
            {
                var result = await _youtubeVideoService.GetYouTubeVideosByCompanyId(companyId);
                return await Task.FromResult(new ResponseModel<object>(result));
            }
            catch (Exception ex)
            {
                return new ExceptionResponse<object>();
            }
        }
        [HttpGet()]
        [Authorize]
        [Route("GetYouTubeVideosByCompanyAndUserId")]
        public async Task<ResponseModel<object>> GetYouTubeVideosByCompanyAndUserId([FromQuery] GetYouTubeVideosByCompanyAndUserIdBindingModel model)
        {
            try
            {
                var result = await _youtubeVideoService.GetYouTubeVideosByCompanyId(model.CompanyId, model.UserId);
                return await Task.FromResult(new ResponseModel<object>(result));
            }
            catch (Exception ex)
            {
                return new ExceptionResponse<object>();
            }
        }
        [HttpGet()]
        [Route("GetYouTubeVideosByCategoryTitle")]
        public async Task<ResponseModel<object>> GetYouTubeVideosByCategoryTitle([FromQuery] string categoryTitle)
        {
            try
            {
                var result = await _youtubeVideoService.GetYouTubeVideosByCategoryTitle(categoryTitle);
                return await Task.FromResult(new ResponseModel<object>(result));
            }
            catch (Exception ex)
            {
                return new ExceptionResponse<object>();
            }
        }
        [HttpGet()]
        [Route("GetCategoryPointsByCompanyAndUserId")]
        public async Task<ResponseModel<object>> GetCategoryPointsByCompanyAndUserId([FromQuery] GetYouTubeVideosByCompanyAndUserIdBindingModel model)
        {
            try
            {
                var result = await _youtubeVideoService.GetCategoryPointsByCompanyAndUserId(model.CompanyId, model.UserId);
                return await Task.FromResult(new ResponseModel<object>(result));
            }
            catch (Exception ex)
            {
                return new ExceptionResponse<object>();
            }
        }
        [HttpPost()]
        [Authorize]
        [Route("DeleteYouTubeVideo")]
        public async Task<ResponseModel<object>> DeleteYouTubeVideo([FromBody] DeleteYouTubeVideoBingdingModel model)
        {
            try
            {

                var result = await _youtubeVideoService.DeleteYouTubeVideo(model.Id);
                return await Task.FromResult(new ResponseModel<object>(result));
            }
            catch (Exception ex)
            {
                return new ExceptionResponse<object>();
            }
        }
        [HttpPost()]
        [Authorize]
        [Route("UpdateYouTubeVideo")]
        public async Task<ResponseModel<object>> UpdateYouTubeVideo([FromBody] UpdateYouTubeVideoBindingModel model)
        {
            try
            {
                var result = await _youtubeVideoService.UpdateYoutubeVideo(model);
                return await Task.FromResult(new ResponseModel<object>(result));
            }
            catch (Exception ex)
            {
                return new ExceptionResponse<object>();
            }
        }
    }
}
