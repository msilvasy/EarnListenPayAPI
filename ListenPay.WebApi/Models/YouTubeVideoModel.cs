using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListenPay.WebApi.Models
{
    public class YouTubeVideoModel
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public string VideoId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ThumbnailURL { get; set; }
        public string VideoURL { get; set; }
        public double WatchTimeStatus { get; set; }
        public DateTime DateCreated { get; set; }
        public List<YouTubeCategoryModel> RelatedCategories { get; set; }
    }
}
