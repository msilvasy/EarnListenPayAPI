using ListenPay.Data;
using ListenPay.Data.Entities;
using ListenPay.WebApi.IService;
using ListenPay.WebApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListenPay.WebApi.Services
{
    public class YouTubeCategoryService : IYoutubeVideoCategory
    {

        private readonly ListenPayDbContext _context;

        public YouTubeCategoryService(ListenPayDbContext context)
        {
            _context = context;
        }

        public async Task<List<CompanyRelatedYouTubeCategoriesModel>> GetYouTubeCategoriesByCompanyId(int companyId)
        {
            if (_context != null)
            {
                return await (from category in _context.CompanyRelatedYouTubeCategories
                              where category.CompanyId == companyId
                              orderby category.YouTubeCategory.YouTubeVideoCategoryId descending
                              select new CompanyRelatedYouTubeCategoriesModel()
                              {
                                  CompanyRelatedCategoryId = category.ComapnyRelatedYouTubeCategoriesId,
                                  YouTubeVideoCategoryId = category.YouTubeCategoryId,
                                  Title = category.YouTubeCategory.Title,
                                  Description = category.YouTubeCategory.Description
                              }
                              ).ToListAsync();
            }
            return null;
        }
        public async Task<List<YouTubeCategoryModel>> GetYouTubeCategories()
        {
            if (_context != null)
            {
                return await (from category in _context.YouTubeVideoCategory
                              select new YouTubeCategoryModel()
                              {
                                  YouTubeVideoCategoryId = category.YouTubeVideoCategoryId,
                                  Title = category.Title,
                                  Description = category.Description
                              }
                              ).ToListAsync();
            }
            return null;
        }
        public async Task<YouTubeVideoCategory> AddYouTubeVideoCategory(string title, string description = "")
        {

            if (_context != null)
            {
                var youTubeVideoCategory = new YouTubeVideoCategory()
                {
                    Title = title,
                    Description = description,
                    DateEntryCreated = DateTime.Now,
                    DateEntryModified = DateTime.Now
                };
                await _context.YouTubeVideoCategory.AddAsync(youTubeVideoCategory);
                await _context.SaveChangesAsync();
                return youTubeVideoCategory;
            }
            return null;
        }

        public async Task<bool> AddCompanyRelatedYouTubeVideoCategory(int companyId, string title, string description)
        {
            int result = 0;
            if (_context != null)
            {
                var tempCategory = await AddYouTubeVideoCategory(title, description);

                var relatedCategory = new CompanyRelatedYouTubeCategories()
                {
                    CompanyId = companyId,
                    YouTubeCategoryId = tempCategory.YouTubeVideoCategoryId,
                    DateEntryCreated = DateTime.Now,
                    DateEntryModified = DateTime.Now
                };
                await _context.CompanyRelatedYouTubeCategories.AddAsync(relatedCategory);
                result = await _context.SaveChangesAsync();
            }
            return result > 0;
        }

        public async Task<bool> DeleteCompanyRelatedYouTubeVideoCategory(int youTubeCategoryId, int relatedCategoryId)
        {
            int result = 0;
            if (_context != null)
            {
                var relatedCategory = _context.CompanyRelatedYouTubeCategories.Find(relatedCategoryId);
                _context.CompanyRelatedYouTubeCategories.Remove(relatedCategory);
                var youTubeCategory = _context.YouTubeVideoCategory.Find(youTubeCategoryId);
                _context.YouTubeVideoCategory.Remove(youTubeCategory);
                result = await _context.SaveChangesAsync();
            }

            return result > 0;
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

        ~YouTubeCategoryService()
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
