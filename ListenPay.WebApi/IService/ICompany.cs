using ListenPay.Data.Entities;
using ListenPay.WebApi.BindingModels;
using ListenPay.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;

namespace ListenPay.WebApi.IService
{
    public interface ICompany
    {
        AuthenticateResponse Authenticate(AuthenticateRequestBindingModel model);
        IEnumerable<Company> GetAll();
        Company GetCompanyById(int id);
        Company GetCompanyByEmail(string email);
        Task<bool> AddCompany(AddCompanyBindingModel model);
        Task<TelePortalAccount> AddUpdateCompanyUserTelePortalAccount(AddCompanyUserTelePortalBindingModel model);
        Task<List<GetCompanyUserTelePortalModel>> GetCompanyUserTelePortalAccount(int companyId);
        Task<bool> DeleteCompanyUserTelePortalAccount(int companyId);
    }
}
