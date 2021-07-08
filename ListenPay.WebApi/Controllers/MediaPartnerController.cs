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
    public class MediaPartnerController : ControllerBase
    {

        private readonly IMediaPartner _mediaPartner;

        public MediaPartnerController(IMediaPartner mediaPartner)
        {
            _mediaPartner = mediaPartner;
        }

        [HttpPost()]
        [Route("AddMediaPartner")]
        public async Task<ResponseModel<object>> AddUserVideoWatchTime([Required] AddMediaPartnerBindingModel model)
        {
            try
            {
                var result = await _mediaPartner.AddMediaPartner(model.CompanyArtist, model.WebSiteURL, model.Email, model.Phone, model.SelectProducts, model.Comments);
                return await Task.FromResult(new ResponseModel<object>(result));
            }
            catch (Exception ex)
            {
                return new ExceptionResponse<object>();
            }
        }
    }
}
