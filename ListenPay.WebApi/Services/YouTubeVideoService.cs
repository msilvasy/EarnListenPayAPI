using ListenPay.Data;
using ListenPay.Data.Entities;
using ListenPay.WebApi.BindingModels;
using ListenPay.WebApi.IService;
using ListenPay.WebApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;


namespace ListenPay.WebApi.Services
{
    public class YoutubeVideoService : IYoutubeVideoService
    {
        private const int WATCH_TIME_THRESHOLD = 90;
        private readonly ListenPayDbContext _context;

        public YoutubeVideoService(ListenPayDbContext context)
        {
            _context = context;
        }
        public async Task<bool> AddYoutubeVideo(AddYouTubeVideoBindingModel model)
        {
            int result = 0;
            if (_context != null)
            {
                var youtubeVideo = new YouTubeVideo()
                {
                    Title = model.Title,
                    CompanyId = model.CompanyId,
                    VideoId = model.VideoId,
                    VideoURL = model.VideoURL,
                    ThumbnailURL = model.ThumbnailURL,
                    Description = model.Description,
                    DurationInSeconds = model.DurationInSeconds,
                    DurationYTformat = model.DurationYTformat,
                    DateEntryCreated = DateTime.Now,
                    DateEntryModified = DateTime.Now
                };

                youtubeVideo.YouTubeVideoRelatedCategories = new List<YouTubeVideoRelatedCategories>();
                foreach (var id in model.RelatedCategoryIds)
                {
                    var category = _context.YouTubeVideoCategory.Find(id);
                    if (category != null)
                        youtubeVideo.YouTubeVideoRelatedCategories.Add(new YouTubeVideoRelatedCategories() { YouTubeVideoCategory = category });
                }
                _context.YouTubeVideo.Add(youtubeVideo);
                result = await _context.SaveChangesAsync();
            }
            return result > 0;
        }

        public async Task<bool> DeleteYouTubeVideo(int id)
        {
            int result = 0;
            if (_context != null)
            {
                var youtubeVideo = await _context.YouTubeVideo.Include(y => y.YouTubeVideoRelatedCategories).SingleOrDefaultAsync(x => x.YouTubeVideoId == id);
                if (youtubeVideo != null)
                {
                    foreach (var category in youtubeVideo.YouTubeVideoRelatedCategories.ToList())
                    {
                        _context.YouTubeVideoRelatedCategories.Remove(category);
                    }
                    _context.YouTubeVideo.Remove(youtubeVideo);
                    result = await _context.SaveChangesAsync();
                }
            }
            return result > 0;
        }

        public async Task<List<YouTubeVideoModel>> GetYouTubeVideosByCompanyId(int companyId)
        {
            if (_context != null)
            {
                return await (
                    from video in _context.YouTubeVideo.Include(y => y.YouTubeVideoRelatedCategories)
                    where video.CompanyId == companyId
                    select new YouTubeVideoModel()
                    {
                        Id = video.YouTubeVideoId,
                        CompanyId = video.CompanyId,
                        Title = video.Title,
                        VideoId = video.VideoId,
                        VideoURL = video.VideoURL,
                        Description = video.Description,
                        ThumbnailURL = video.ThumbnailURL,
                        DateCreated = (DateTime)video.DateEntryCreated,
                        RelatedCategories = (List<YouTubeCategoryModel>)video.YouTubeVideoRelatedCategories.Select(x => x.YouTubeVideoCategory).Select(p => new YouTubeCategoryModel() { YouTubeVideoCategoryId = p.YouTubeVideoCategoryId, Title = p.Title })

                    }).ToListAsync();
            }
            return null;
        }
        public async Task<List<YouTubeVideoModel>> GetYouTubeVideosByCompanyId(int companyId, int userId)
        {
            if (_context != null)
            {
                var youTubeVideos = await (
                    from video in _context.YouTubeVideo.Include(y => y.YouTubeVideoRelatedCategories)
                    where video.CompanyId == companyId
                    select new YouTubeVideoModel()
                    {
                        Id = video.YouTubeVideoId,
                        CompanyId = video.CompanyId,
                        Title = video.Title,
                        VideoId = video.VideoId,
                        VideoURL = video.VideoURL,
                        Description = video.Description,
                        ThumbnailURL = video.ThumbnailURL,
                        DateCreated = (DateTime)video.DateEntryCreated,
                        RelatedCategories = (List<YouTubeCategoryModel>)video.YouTubeVideoRelatedCategories.Select(x => x.YouTubeVideoCategory).Select(p => new YouTubeCategoryModel() { YouTubeVideoCategoryId = p.YouTubeVideoCategoryId, Title = p.Title })

                    }).ToListAsync();
                foreach (var video in youTubeVideos)
                {
                    var activity = (from uWatch in _context.UserVideoWatchActivity.Include(y => y.YouTubeVideo) where uWatch.UserInformationId == userId && video.Id == uWatch.YouTubeVideoId select new { WatchTimeInSeconds = uWatch.WatchTimeInSeconds, VideoDuratonInSecond = uWatch.YouTubeVideo.DurationInSeconds }).ToList();
                    if (activity.Count() > 0)
                    {

                        var watchStatus = (activity.FirstOrDefault().WatchTimeInSeconds / activity.FirstOrDefault().VideoDuratonInSecond) * 100;
                        if (watchStatus > WATCH_TIME_THRESHOLD)
                        {
                            video.WatchTimeStatus = 100;
                        }
                        else
                        {
                            video.WatchTimeStatus = watchStatus;
                        }
                    }
                }
                return youTubeVideos;
            }
            return null;
        }
        public async Task<List<YouTubeVideoModel>> GetYouTubeVideosByCategoryTitle(string categoryTitle)
        {
            if (_context != null)
            {
                var youTubeVideos = await (
                    from video in _context.YouTubeVideo.Include(y => y.YouTubeVideoRelatedCategories)
                    where video.YouTubeVideoRelatedCategories.Any(p=>p.YouTubeVideoCategory.Title == categoryTitle)
                    select new YouTubeVideoModel()
                    {
                        Id = video.YouTubeVideoId,
                        CompanyId = video.CompanyId,
                        Title = video.Title,
                        VideoId = video.VideoId,
                        VideoURL = video.VideoURL,
                        Description = video.Description,
                        ThumbnailURL = video.ThumbnailURL,
                        DateCreated = (DateTime)video.DateEntryCreated,
                        RelatedCategories = (List<YouTubeCategoryModel>)video.YouTubeVideoRelatedCategories.Select(x => x.YouTubeVideoCategory).Select(p => new YouTubeCategoryModel() { YouTubeVideoCategoryId = p.YouTubeVideoCategoryId, Title = p.Title })

                    }).ToListAsync();            
                return youTubeVideos;
            }
            return null;
        }

        public async Task<List<CategoryPoint>> GetCategoryPointsByCompanyAndUserId(int companyId, int userId)
        {

            var categorypointList = new List<CategoryPoint>();
            var youTubeVideos = await GetYouTubeVideosByCompanyId(companyId, userId) ?? new List<YouTubeVideoModel>();
            var categories = await (from category in _context.CompanyRelatedYouTubeCategories.Include(y => y.YouTubeCategory)
                                    where category.CompanyId == companyId
                                    select category).ToListAsync();

            foreach (var category in categories)
            {
                var points = youTubeVideos.Where(x => x.RelatedCategories.Any(y => y.Title.ToLower().Trim() == category.YouTubeCategory.Title.ToLower().Trim())).Count() * 100;
                categorypointList.Add(new CategoryPoint() { CategoryId = category.YouTubeCategoryId, Title = category.YouTubeCategory.Title, Points = points });
            }
            return categorypointList;
        }
        public async Task<bool> UpdateYoutubeVideo(UpdateYouTubeVideoBindingModel model)
        {

            bool result = false;
            if (_context != null)
            {
                var youTubeVideo = _context.YouTubeVideo.Include(y => y.YouTubeVideoRelatedCategories).FirstOrDefault(x => x.YouTubeVideoId == model.YouTubeVideoId);
                foreach (var category in youTubeVideo.YouTubeVideoRelatedCategories.ToList())
                {
                    _context.YouTubeVideoRelatedCategories.Remove(category);
                }
                youTubeVideo.YouTubeVideoRelatedCategories = new List<YouTubeVideoRelatedCategories>();
                foreach (var id in model.RelatedCategoryIds)
                {
                    var category = _context.YouTubeVideoCategory.Find(id);
                    if (category != null)
                        youTubeVideo.YouTubeVideoRelatedCategories.Add(new YouTubeVideoRelatedCategories() { YouTubeVideoCategory = category });
                }
                if (!string.IsNullOrEmpty(model.Title))
                {
                    youTubeVideo.Title = model.Title;
                }
                if (!string.IsNullOrEmpty(model.Description))
                {
                    youTubeVideo.Description = model.Description;
                }
                youTubeVideo.DateEntryModified = DateTime.Now;
                youTubeVideo.UserModified = model.UserId;
                _context.YouTubeVideo.Update(youTubeVideo);
                result = await _context.SaveChangesAsync() > 0;
            }
            return result;
        }

        #region IDisposable Support
        private bool _disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                }
                _context.DisposeAsync();
                _disposedValue = true;
            }
        }

        ~YoutubeVideoService()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(false);
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
