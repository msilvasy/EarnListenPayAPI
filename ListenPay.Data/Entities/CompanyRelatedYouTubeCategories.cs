using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ListenPay.Data.Entities
{
    public class CompanyRelatedYouTubeCategories : BaseEntity
    {
        [Key]
        public int ComapnyRelatedYouTubeCategoriesId { get; set; }
        public int YouTubeCategoryId { get; set; }
        public YouTubeVideoCategory YouTubeCategory { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }
    }
}
