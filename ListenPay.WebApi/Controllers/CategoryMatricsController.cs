using System;
using System.Collections.Generic;
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
    public class CategoryMatricsController : ControllerBase
    {

        private ICategoryMatricsService _categoryMatricsService;

        public CategoryMatricsController(ICategoryMatricsService categoryMatricsService)
        {
            _categoryMatricsService = categoryMatricsService;
        }

        [HttpGet()]
        [Authorize]
        [Route("GetCategoryMatricsById")]
        public async Task<ResponseModel<object>> GetCategoryMatricsById([FromQuery] GetCategoryMatricsByBindingModel model)
        {
            try
            {
                var response = _categoryMatricsService.GetCategoryMatricsById(model.CompanyId, model.CategoryId);
                return await Task.FromResult(new ResponseModel<object>(response.Result));
            }
            catch (Exception ex)
            {
                return new ExceptionResponse<object>();

            }
        }

    }
}
