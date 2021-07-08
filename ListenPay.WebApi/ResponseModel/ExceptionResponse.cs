using ListenPay.WebApi.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListenPay.WebApi.ResponseModel
{
    public class ExceptionResponse<T> : ResponseModel<T> where T : class
    {
        public ExceptionResponse(ResponseCode responseCode) : base(Constants.INTERNAL_SERVER_ERROR, responseCode)
        {

        }
        public ExceptionResponse() : base(Constants.INTERNAL_SERVER_ERROR, ResponseCode.Error)
        {

        }
        public ExceptionResponse(string responseMessage) : base(responseMessage, ResponseCode.Error)
        { }
    }
}
