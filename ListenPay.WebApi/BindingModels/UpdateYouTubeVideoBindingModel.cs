using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListenPay.WebApi.BindingModels
{
    public class UpdateYouTubeVideoBindingModel
    {
        public int UserId { get; set; }
        public int YouTubeVideoId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<int> RelatedCategoryIds { get; set; }
    }
}
