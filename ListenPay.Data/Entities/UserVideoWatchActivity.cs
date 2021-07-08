using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ListenPay.Data.Entities
{
    public class UserVideoWatchActivity : BaseEntity
    {

        [Key]
        public int Id { get; set; }
        public int UserInformationId { get; set; }
        public UserInformation UserInformation { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }
        public int YouTubeVideoId { get; set; }
        public YouTubeVideo YouTubeVideo { get; set; }
        public double WatchTimeInSeconds { get; set; }
    }
}
