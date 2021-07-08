using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListenPay.WebApi.ResponseModel
{
    public class ResponseModel<T> where T : class
    {
        public DateTime ResponseTime { get; set; }
        public ResponseCode ResponseCode { get; set; }
        public string ResponseMessage { get; set; }
        public T DataSet { get; set; }

        ResponseModel()
        {
            ResponseTime = DateTime.Now;
            DataSet = (T)Activator.CreateInstance(typeof(T));
            ResponseMessage = string.Empty;
        }
        public ResponseModel(string responseMessage, ResponseCode responseCode)
        {
            ResponseTime = DateTime.Now;
            DataSet = (T)Activator.CreateInstance(typeof(T));
            ResponseMessage = responseMessage;
            ResponseCode = responseCode;
        }

        public ResponseModel(T data, string responseMessage = "", ResponseCode responseCode = ResponseCode.OK)
        {
            ResponseTime = DateTime.Now;
            DataSet = data;
            ResponseMessage = responseMessage;
            ResponseCode = responseCode;
        }
    }
}
