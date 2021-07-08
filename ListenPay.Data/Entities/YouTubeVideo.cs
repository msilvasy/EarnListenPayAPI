using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ListenPay.Data.Entities
{
    public class YouTubeVideo : BaseEntity
    {
        [Key]
        public int YouTubeVideoId { get; set; }
        [Required]
        public string VideoId { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public string ThumbnailURL { get; set; }
        public double DurationInSeconds { get; set; }
        public string DurationYTformat { get; set; }
        [Required]
        public string VideoURL { get; set; }
        [Required]
        public int CompanyId { get; set; }
        public Company Company { get; set; }
        public virtual ICollection<YouTubeVideoRelatedCategories> YouTubeVideoRelatedCategories { get; set; }
    }
}
