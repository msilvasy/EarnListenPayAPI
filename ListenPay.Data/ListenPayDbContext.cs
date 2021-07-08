using ListenPay.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ListenPay.Data
{
    public class ListenPayDbContext : DbContext
    {
        public ListenPayDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Entities.Country>();
            
            var userEntity = modelBuilder.Entity<Entities.UserInformation>();
            userEntity.Property(u=> u.Earned).HasColumnType("money");
            userEntity.Property(u => u.Discount).HasColumnType("money");

            var activityLogEntity = modelBuilder.Entity<Entities.ActivityLog>();
            activityLogEntity.HasOne(a => a.User).WithMany().HasForeignKey(u => u.UserId);
            activityLogEntity.Property(a => a.Earned).HasColumnType("money");
            activityLogEntity.Property(a => a.OldEarned).HasColumnType("money");
            activityLogEntity.Property(a => a.CurrentEarned).HasColumnType("money");

            modelBuilder.Entity<TelePortalAccount>();


            base.OnModelCreating(modelBuilder);
        }

        public DbSet<YouTubeVideo> YouTubeVideo { get; set; }
        public DbSet<CompanyRelatedYouTubeCategories> CompanyRelatedYouTubeCategories { get; set; }
        public DbSet<YouTubeVideoCategory> YouTubeVideoCategory { get; set; }
        public DbSet<YouTubeVideoRelatedCategories> YouTubeVideoRelatedCategories { get; set; }
        public DbSet<ActivityLog> ActivityLog { get; set; }
        public DbSet<Company> Company { get; set; }
        public DbSet<UserInformation> UserInformation { get; set; }
        public DbSet<TelePortalAccount> TelePortalAccount { get; set; }
        public DbSet<UserVideoWatchActivity> UserVideoWatchActivity { get; set; }
        public DbSet<PaymentMethod> PaymentMethod { get; set; }
        public DbSet<CategoryMatrics> CategoryMatrics { get; set; }
        public DbSet<UserRelatedVideoData> UserRelatedVideoData { get; set; }
        public DbSet<MediaPartner> MediaPartner { get; set; }
        public DbSet<MediaPartnerProduct> MediaPartnerProduct { get; set; }
    }
}
