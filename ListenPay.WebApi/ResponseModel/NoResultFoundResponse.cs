using ListenPay.WebApi.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListenPay.WebApi.ResponseModel
{
    public class NoResultFoundResponse<T> : ResponseModel<T> where T : class
    {
        public NoResultFoundResponse() : base(Constants.NO_DATA_FOUND, ResponseCode.OK)
        {

        }
        public NoResultFoundResponse(ResponseCode responseCode) : base(Constants.NO_DATA_FOUND, responseCode)
        {

        }
        public NoResultFoundResponse(string responseMessage) : base(responseMessage, ResponseCode.Error)
        { }
        public NoResultFoundResponse(string responseMessage, ResponseCode responseCode) : base(responseMessage, responseCode)
        { }
    }
}
