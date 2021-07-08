using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListenPay.WebApi.ResponseModel
{
    public enum ResponseCode
    {
        OK = 1,
        Error = 2,
        Unauthorized = 401,
        NoDataFound = 4
    }
}
