using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ListenPay.Data.Entities
{
    public class Company : BaseEntity
    {
        [Key]
        public int CompanyId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string Address { get; set; }
        public string StreetAddress { get; set; }
        [Required]
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Unit { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        [Required]
        public string Password { get; set; }
        public string LastPassword { get; set; }
        public virtual ICollection<TelePortalAccount> CompanyRelatedUsers { get; set; }
        public virtual ICollection<YouTubeVideo> YouTubeVideos { get; set; }
        public virtual ICollection<CompanyRelatedYouTubeCategories> CompanyRelatedYouTubeCategories { get; set; }
        public virtual ICollection<UserVideoWatchActivity> UserVideoWatchActivities { get; set; }
        public virtual ICollection<ActivityLog> ActivityLogs { get; set; }
        public virtual ICollection<CategoryMatrics> CategoryMatrics { get; set; }

    }
}
