﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListenPay.WebApi.Models
{
    public class CompanyRelatedYouTubeCategoriesModel
    {
        public int CompanyRelatedCategoryId { get; set; }
        public int YouTubeVideoCategoryId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
