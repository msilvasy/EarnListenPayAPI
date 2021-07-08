using ListenPay.Data.Entities;
using ListenPay.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListenPay.WebApi.IService
{
    public interface IYoutubeVideoCategory
    {
        Task<List<CompanyRelatedYouTubeCategoriesModel>> GetYouTubeCategoriesByCompanyId(int userInformationId);
        Task<List<YouTubeCategoryModel>> GetYouTubeCategories();
        Task<YouTubeVideoCategory> AddYouTubeVideoCategory(string title, string description = "");
        Task<bool> AddCompanyRelatedYouTubeVideoCategory(int userInformationId, string title, string description);
        Task<bool> DeleteCompanyRelatedYouTubeVideoCategory(int youTubeCategoryId, int relatedCategoryId);

    }
}
