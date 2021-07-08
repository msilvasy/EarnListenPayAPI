using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ListenPay.Data.Entities
{
    public class YouTubeVideoRelatedCategories : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public int YouTubeVideoCategoryId { get; set; }
        public YouTubeVideoCategory YouTubeVideoCategory { get; set; }
        public int YouTubeVideoId { get; set; }
        public YouTubeVideo YouTubeVideo { get; set; }
    }
}
