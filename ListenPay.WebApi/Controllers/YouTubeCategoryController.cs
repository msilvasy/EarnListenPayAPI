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
    public class YouTubeCategoryController : ControllerBase
    {
        private readonly IYoutubeVideoCategory _categoryService;

        public YouTubeCategoryController(IYoutubeVideoCategory category)
        {
            _categoryService = category;
        }
        [HttpGet()]
        [Authorize]
        [Route("GetYouTubeCategoriesByCompanyId")]
        public async Task<ResponseModel<object>> GetYouTubeCategoriesByCompanyId([Required] int companyId)
        {
            try
            {
                var result = await _categoryService.GetYouTubeCategoriesByCompanyId(companyId);
                return await Task.FromResult(new ResponseModel<object>(result));
            }
            catch (Exception ex)
            {
                return new ExceptionResponse<object>();
            }
        }
        [HttpGet()]
        [Authorize]
        [Route("GetYouTubeVideoCategories")]
        public async Task<ResponseModel<object>> GetYouTubeVideoCategories()
        {
            try
            {
                var result = await _categoryService.GetYouTubeCategories();
                return await Task.FromResult(new ResponseModel<object>(result));
            }
            catch (Exception ex)
            {
                return new ExceptionResponse<object>();
            }
        }
        [HttpPost()]
        [Authorize]
        [Route("AddYouTubeVideoCategory")]
        public async Task<ResponseModel<object>> AddYouTubeVideoCategory([FromBody] AddYouTubeCategoryBindingModel model)
        {
            try
            {
                var result = await _categoryService.AddYouTubeVideoCategory(model.Title, model.Description);
                return await Task.FromResult(new ResponseModel<object>(result));
            }
            catch (Exception ex)
            {
                return new ExceptionResponse<object>();
            }
        }
        [HttpPost()]
        [Authorize]
        [Route("AddCompanyRelatedYouTubeCategory")]
        public async Task<ResponseModel<object>> AddCompanyRelatedYouTubeCategory([FromBody] AddCompanyRelatedYouTubeCategory model)
        {
            try
            {
                var result = await _categoryService.AddCompanyRelatedYouTubeVideoCategory(model.CompanyId, model.Title,model.Description);
                return await Task.FromResult(new ResponseModel<object>(result));
            }
            catch (Exception ex)
            {
                return new ExceptionResponse<object>();
            }
        }
        [HttpPost()]
        [Authorize]
        [Route("DeleteCompanyRelatedYouTubeCategory")]
        public async Task<ResponseModel<object>> DeleteCompanyRelatedYouTubeCategory([FromBody] DeleteCompanyRelatedYouTubeCategory model)
        {
            try
            {
                var result = await _categoryService.DeleteCompanyRelatedYouTubeVideoCategory(model.YouTubeCategoryId, model.RelatedCategoryId);
                return await Task.FromResult(new ResponseModel<object>(result));
            }
            catch (Exception ex)
            {
                return new ExceptionResponse<object>();
            }
        }
    }
}
