﻿// <auto-generated />
using System;
using ListenPay.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ListenPay.Data.Migrations
{
    [DbContext(typeof(ListenPayDbContext))]
    [Migration("20200927073049_mg6")]
    partial class mg6
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ListenPay.Data.Entities.ActivityLog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ActivityType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("CurrentEarned")
                        .HasColumnType("money");

                    b.Property<DateTime?>("DateEntryCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateEntryModified")
                        .HasColumnType("datetime2");

                    b.Property<double>("Duration")
                        .HasColumnType("float");

                    b.Property<decimal>("Earned")
                        .HasColumnType("money");

                    b.Property<string>("NextMediaPlayed")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("OldEarned")
                        .HasColumnType("money");

                    b.Property<DateTime>("TimeStamp")
                        .HasColumnType("datetime2");

                    b.Property<string>("TrackId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TrackName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserCreated")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int?>("UserModified")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("ActivityLog");
                });

            modelBuilder.Entity("ListenPay.Data.Entities.Company", b =>
                {
                    b.Property<int>("CompanyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DateEntryCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateEntryModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastPassword")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Mobile")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("State")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StreetAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Unit")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserCreated")
                        .HasColumnType("int");

                    b.Property<int?>("UserModified")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Zip")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CompanyId");

                    b.ToTable("Company");
                });

            modelBuilder.Entity("ListenPay.Data.Entities.CompanyRelatedYouTubeCategories", b =>
                {
                    b.Property<int>("UserRelatedYouTubeCategoriesId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CompanyId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DateEntryCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateEntryModified")
                        .HasColumnType("datetime2");

                    b.Property<int?>("UserCreated")
                        .HasColumnType("int");

                    b.Property<int?>("UserModified")
                        .HasColumnType("int");

                    b.Property<int>("YouTubeCategoryId")
                        .HasColumnType("int");

                    b.HasKey("UserRelatedYouTubeCategoriesId");

                    b.HasIndex("CompanyId");

                    b.HasIndex("YouTubeCategoryId");

                    b.ToTable("CompanyRelatedYouTubeCategories");
                });

            modelBuilder.Entity("ListenPay.Data.Entities.Country", b =>
                {
                    b.Property<int>("CountryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DateEntryCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateEntryModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserCreated")
                        .HasColumnType("int");

                    b.Property<int?>("UserModified")
                        .HasColumnType("int");

                    b.HasKey("CountryId");

                    b.ToTable("Country");
                });

            modelBuilder.Entity("ListenPay.Data.Entities.TelePortalAccount", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ConfirmPassword")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CurrentPassword")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DateEntryCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateEntryModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("FullName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GroupNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InsuranceCarrier")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MemberId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NewPassword")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("State")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StreetAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Unit")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserCreated")
                        .HasColumnType("int");

                    b.Property<int?>("UserInformationId")
                        .HasColumnType("int");

                    b.Property<int?>("UserModified")
                        .HasColumnType("int");

                    b.Property<string>("Zip")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.HasIndex("UserInformationId");

                    b.ToTable("TelePortalAccount");
                });

            modelBuilder.Entity("ListenPay.Data.Entities.UserInformation", b =>
                {
                    b.Property<int>("UserInformationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ConfirmPassword")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("CountryId")
                        .HasColumnType("int");

                    b.Property<string>("CurrentPassword")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DateEntryCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateEntryModified")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Discount")
                        .HasColumnType("money");

                    b.Property<decimal?>("Earned")
                        .HasColumnType("money");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Key")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NewPassword")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("State")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserCreated")
                        .HasColumnType("int");

                    b.Property<int?>("UserModified")
                        .HasColumnType("int");

                    b.Property<string>("Zip")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserInformationId");

                    b.HasIndex("CountryId");

                    b.ToTable("UserInformation");
                });

            modelBuilder.Entity("ListenPay.Data.Entities.YouTubeVideo", b =>
                {
                    b.Property<int>("YouTubeVideoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("DateEntryCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateEntryModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ThumbnailURL")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserCreated")
                        .HasColumnType("int");

                    b.Property<int?>("UserModified")
                        .HasColumnType("int");

                    b.Property<string>("VideoId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VideoURL")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("YouTubeVideoId");

                    b.ToTable("YouTubeVideo");
                });

            modelBuilder.Entity("ListenPay.Data.Entities.YouTubeVideoCategory", b =>
                {
                    b.Property<int>("YouTubeVideoCategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("DateEntryCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateEntryModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserCreated")
                        .HasColumnType("int");

                    b.Property<int?>("UserModified")
                        .HasColumnType("int");

                    b.HasKey("YouTubeVideoCategoryId");

                    b.ToTable("YouTubeVideoCategory");
                });

            modelBuilder.Entity("ListenPay.Data.Entities.YouTubeVideoRelatedCategories", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("DateEntryCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateEntryModified")
                        .HasColumnType("datetime2");

                    b.Property<int?>("UserCreated")
                        .HasColumnType("int");

                    b.Property<int?>("UserModified")
                        .HasColumnType("int");

                    b.Property<int>("YouTubeVideoCategoryId")
                        .HasColumnType("int");

                    b.Property<int>("YouTubeVideoId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("YouTubeVideoCategoryId");

                    b.HasIndex("YouTubeVideoId");

                    b.ToTable("YouTubeVideoRelatedCategories");
                });

            modelBuilder.Entity("ListenPay.Data.Entities.ActivityLog", b =>
                {
                    b.HasOne("ListenPay.Data.Entities.UserInformation", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ListenPay.Data.Entities.CompanyRelatedYouTubeCategories", b =>
                {
                    b.HasOne("ListenPay.Data.Entities.Company", "Company")
                        .WithMany()
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ListenPay.Data.Entities.YouTubeVideoCategory", "YouTubeCategory")
                        .WithMany("CopmanyRelatedYouTubeCategories")
                        .HasForeignKey("YouTubeCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ListenPay.Data.Entities.TelePortalAccount", b =>
                {
                    b.HasOne("ListenPay.Data.Entities.UserInformation", "UserInformation")
                        .WithMany()
                        .HasForeignKey("UserInformationId");
                });

            modelBuilder.Entity("ListenPay.Data.Entities.UserInformation", b =>
                {
                    b.HasOne("ListenPay.Data.Entities.Country", "Country")
                        .WithMany()
                        .HasForeignKey("CountryId");
                });

            modelBuilder.Entity("ListenPay.Data.Entities.YouTubeVideoRelatedCategories", b =>
                {
                    b.HasOne("ListenPay.Data.Entities.YouTubeVideoCategory", "YouTubeVideoCategory")
                        .WithMany()
                        .HasForeignKey("YouTubeVideoCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ListenPay.Data.Entities.YouTubeVideo", "YouTubeVideo")
                        .WithMany("YouTubeVideoRelatedCategories")
                        .HasForeignKey("YouTubeVideoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}