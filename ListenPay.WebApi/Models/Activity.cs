using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ListenPay.WebApi.Models
{
    public class Activity
    {
        public string TrackId { get; set; }

        public string TrackName { get; set; }

        public double Duration { get; set; }

        public string ActivityType { get; set; }

        public string NextMediaPlayed { get; set; }
        [Required]
        public int CompanyId { get; set; }
        public int YouTubeVideoId { get; set; }
        public int CategoryId { get; set; }
        public int UserInformationId { get; set; }
    }
}
