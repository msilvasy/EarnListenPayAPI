using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListenPay.WebApi.BindingModels
{
    public class AddYouTubeVideoBindingModel
    {
        public int CompanyId { get; set; }
        public string VideoId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ThumbnailURL { get; set; }
        public int DurationInSeconds { get; set; }
        public string DurationYTformat { get; set; }
        public string VideoURL { get; set; }
        public List<int> RelatedCategoryIds { get; set; }
    }
}
