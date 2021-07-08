using ListenPay.WebApi.BindingModels;
using ListenPay.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListenPay.WebApi.IService
{
    public interface IYoutubeVideoService
    {
        Task<List<YouTubeVideoModel>> GetYouTubeVideosByCompanyId(int companyId);
        Task<List<YouTubeVideoModel>> GetYouTubeVideosByCompanyId(int companyId, int userId);
        Task<List<CategoryPoint>> GetCategoryPointsByCompanyAndUserId(int companyId, int userId);
        Task<bool> DeleteYouTubeVideo(int userInformationId);
        Task<bool> AddYoutubeVideo(AddYouTubeVideoBindingModel model);
        Task<bool> UpdateYoutubeVideo(UpdateYouTubeVideoBindingModel model);
        Task<List<YouTubeVideoModel>> GetYouTubeVideosByCategoryTitle(string categoryTitle);
    }
}
