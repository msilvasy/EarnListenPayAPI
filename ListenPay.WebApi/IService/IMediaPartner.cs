using ListenPay.WebApi.BindingModels;
using ListenPay.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListenPay.WebApi.IService
{
    public interface IMediaPartner
    {
        Task<bool> AddMediaPartner(string companyArtist, string WebSiteURL, string email, string phone, string selectedProduct, string comments);
    }
}
