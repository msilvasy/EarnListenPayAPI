using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ListenPay.Data.Entities
{
    public class YouTubeVideoCategory : BaseEntity
    {
        [Key]
        public int YouTubeVideoCategoryId { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public virtual ICollection<CompanyRelatedYouTubeCategories> CopmanyRelatedYouTubeCategories { get; set; }
    }
}
