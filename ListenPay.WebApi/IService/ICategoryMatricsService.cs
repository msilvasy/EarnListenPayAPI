using ListenPay.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListenPay.WebApi.IService
{
    public interface ICategoryMatricsService
    {
        Task<CategoryMatricsDataSetModel> GetCategoryMatricsById(int companyId, int categoryId);
    }
}
