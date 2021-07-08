using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ListenPay.Data.Entities
{
    public class UserRelatedVideoData : BaseEntity
    {
        [Key]
        public int UserRelatedDataId { get; set; }
        public int UserInformationId { get; set; }
        public UserInformation UserInformation { get; set; }
        public double Duration { get; set; }
        public int YouTubeVideoId { get; set; }
        public YouTubeVideo YouTubeVideo { get; set; }
        public int YouTubeVideoCategoryId { get; set; }
        public YouTubeVideoCategory YouTubeVideoCategory { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }
    }
}
