﻿// <auto-generated />
using System;
using Kubex.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Kubex.DAL.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20200221135544_AddedCompanyFKToContactTable")]
    partial class AddedCompanyFKToContactTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Kubex.Models.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("AppartementBus")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("CountryId")
                        .HasColumnType("int");

                    b.Property<byte?>("CountryId1")
                        .HasColumnType("tinyint unsigned");

                    b.Property<int>("HouseNumber")
                        .HasColumnType("int");

                    b.Property<int>("StreetId")
                        .HasColumnType("int");

                    b.Property<int>("ZIPId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CountryId1");

                    b.HasIndex("StreetId");

                    b.HasIndex("ZIPId");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("Kubex.Models.Checkpoint", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<byte>("CheckpointTypeId")
                        .HasColumnType("tinyint unsigned");

                    b.Property<string>("Description")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Identifier")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int?>("LandmapId")
                        .HasColumnType("int");

                    b.Property<int>("LocationId")
                        .HasColumnType("int");

                    b.Property<bool>("Mandatory")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("Id");

                    b.HasIndex("CheckpointTypeId");

                    b.HasIndex("LandmapId");

                    b.HasIndex("LocationId");

                    b.ToTable("Checkpoints");
                });

            modelBuilder.Entity("Kubex.Models.CheckpointType", b =>
                {
                    b.Property<byte>("Id")
                        .HasColumnType("tinyint unsigned");

                    b.Property<string>("Type")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("CheckpointTypes");
                });

            modelBuilder.Entity("Kubex.Models.Company", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("AddressId")
                        .HasColumnType("int");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("LogoUrl")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.ToTable("Companies");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Company");
                });

            modelBuilder.Entity("Kubex.Models.Contact", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CompanyId")
                        .HasColumnType("int");

                    b.Property<byte>("ContactTypeId")
                        .HasColumnType("tinyint unsigned");

                    b.Property<int?>("PostId")
                        .HasColumnType("int");

                    b.Property<int>("Sequence")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("Value")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.HasIndex("ContactTypeId");

                    b.HasIndex("PostId");

                    b.HasIndex("UserId");

                    b.ToTable("Contacts");
                });

            modelBuilder.Entity("Kubex.Models.ContactType", b =>
                {
                    b.Property<byte>("Id")
                        .HasColumnType("tinyint unsigned");

                    b.Property<string>("Type")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("ContactTypes");
                });

            modelBuilder.Entity("Kubex.Models.Country", b =>
                {
                    b.Property<byte>("Id")
                        .HasColumnType("tinyint unsigned");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("Kubex.Models.DailyActivityReport", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("DailyActivityReports");
                });

            modelBuilder.Entity("Kubex.Models.Entry", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("DailyActivityReportId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<byte>("EntryTypeId")
                        .HasColumnType("tinyint unsigned");

                    b.Property<int?>("LocationId")
                        .HasColumnType("int");

                    b.Property<DateTime>("OccuranceDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("ParentEntryId")
                        .HasColumnType("int");

                    b.Property<byte>("PriorityId")
                        .HasColumnType("tinyint unsigned");

                    b.HasKey("Id");

                    b.HasIndex("DailyActivityReportId");

                    b.HasIndex("EntryTypeId");

                    b.HasIndex("LocationId");

                    b.HasIndex("ParentEntryId");

                    b.HasIndex("PriorityId");

                    b.ToTable("Entries");
                });

            modelBuilder.Entity("Kubex.Models.EntryType", b =>
                {
                    b.Property<byte>("Id")
                        .HasColumnType("tinyint unsigned");

                    b.Property<string>("Type")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("EntryTypes");
                });

            modelBuilder.Entity("Kubex.Models.Landmap", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("FloorplanUrl")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("Landmaps");
                });

            modelBuilder.Entity("Kubex.Models.License", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<byte>("LicenseTypeId")
                        .HasColumnType("tinyint unsigned");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex("LicenseTypeId");

                    b.HasIndex("UserId");

                    b.ToTable("Licenses");
                });

            modelBuilder.Entity("Kubex.Models.LicenseType", b =>
                {
                    b.Property<byte>("Id")
                        .HasColumnType("tinyint unsigned");

                    b.Property<TimeSpan>("Duration")
                        .HasColumnType("time(6)");

                    b.Property<string>("Type")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("LicenseTypes");
                });

            modelBuilder.Entity("Kubex.Models.Location", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("GPSData")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("Location");
                });

            modelBuilder.Entity("Kubex.Models.Media", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("EntryId")
                        .HasColumnType("int");

                    b.Property<byte>("EntryTypeId")
                        .HasColumnType("tinyint unsigned");

                    b.Property<byte>("MediaTypeId")
                        .HasColumnType("tinyint unsigned");

                    b.Property<string>("Url")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex("EntryId");

                    b.HasIndex("EntryTypeId");

                    b.HasIndex("MediaTypeId");

                    b.ToTable("Media");
                });

            modelBuilder.Entity("Kubex.Models.MediaType", b =>
                {
                    b.Property<byte>("Id")
                        .HasColumnType("tinyint unsigned");

                    b.Property<string>("Type")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("MediaTypes");
                });

            modelBuilder.Entity("Kubex.Models.Mission", b =>
                {
                    b.Property<int>("CheckpointId")
                        .HasColumnType("int");

                    b.Property<int>("RoundId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<TimeSpan>("Duration")
                        .HasColumnType("time(6)");

                    b.Property<byte>("MissionTypeId")
                        .HasColumnType("tinyint unsigned");

                    b.HasKey("CheckpointId", "RoundId");

                    b.HasIndex("MissionTypeId");

                    b.HasIndex("RoundId");

                    b.ToTable("Missions");
                });

            modelBuilder.Entity("Kubex.Models.MissionType", b =>
                {
                    b.Property<byte>("Id")
                        .HasColumnType("tinyint unsigned");

                    b.Property<string>("Type")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("MissionTypes");
                });

            modelBuilder.Entity("Kubex.Models.Post", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("AddressId")
                        .HasColumnType("int");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<int?>("LocationId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("LocationId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("Kubex.Models.Priority", b =>
                {
                    b.Property<byte>("Id")
                        .HasColumnType("tinyint unsigned");

                    b.Property<string>("Level")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("Priorities");
                });

            modelBuilder.Entity("Kubex.Models.Round", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<TimeSpan>("Duration")
                        .HasColumnType("time(6)");

                    b.Property<int?>("EntryId")
                        .HasColumnType("int");

                    b.Property<int>("LandmapId")
                        .HasColumnType("int");

                    b.Property<int>("PostId")
                        .HasColumnType("int");

                    b.Property<byte>("RoundStatusId")
                        .HasColumnType("tinyint unsigned");

                    b.Property<TimeSpan>("StartTime")
                        .HasColumnType("time(6)");

                    b.Property<string>("Title")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex("EntryId");

                    b.HasIndex("LandmapId");

                    b.HasIndex("PostId");

                    b.HasIndex("RoundStatusId");

                    b.ToTable("Rounds");
                });

            modelBuilder.Entity("Kubex.Models.RoundStatus", b =>
                {
                    b.Property<byte>("Id")
                        .HasColumnType("tinyint unsigned");

                    b.Property<string>("Status")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("RoundStatuses");
                });

            modelBuilder.Entity("Kubex.Models.Street", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("Streets");
                });

            modelBuilder.Entity("Kubex.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<int>("AddressId")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Email")
                        .HasColumnType("varchar(256) CHARACTER SET utf8mb4")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("FirstName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("LastName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("varchar(256) CHARACTER SET utf8mb4")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("varchar(256) CHARACTER SET utf8mb4")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UserName")
                        .HasColumnType("varchar(256) CHARACTER SET utf8mb4")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");

                    b.HasDiscriminator<string>("Discriminator").HasValue("User");
                });

            modelBuilder.Entity("Kubex.Models.UserPost", b =>
                {
                    b.Property<int>("PostId")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.HasKey("PostId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("UserPosts");
                });

            modelBuilder.Entity("Kubex.Models.ZIP", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Code")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("ZIPCodes");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(256) CHARACTER SET utf8mb4")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasColumnType("varchar(256) CHARACTER SET utf8mb4")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("RoleId")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("Value")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Kubex.Models.Customer", b =>
                {
                    b.HasBaseType("Kubex.Models.Company");

                    b.Property<string>("ContractNumber")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasDiscriminator().HasValue("Customer");
                });

            modelBuilder.Entity("Kubex.Models.Agent", b =>
                {
                    b.HasBaseType("Kubex.Models.User");

                    b.Property<string>("DepartmentNumber")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("EmployeeNumber")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasDiscriminator().HasValue("Agent");
                });

            modelBuilder.Entity("Kubex.Models.Address", b =>
                {
                    b.HasOne("Kubex.Models.Country", "Country")
                        .WithMany()
                        .HasForeignKey("CountryId1");

                    b.HasOne("Kubex.Models.Street", "Street")
                        .WithMany()
                        .HasForeignKey("StreetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Kubex.Models.ZIP", "ZIP")
                        .WithMany()
                        .HasForeignKey("ZIPId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Kubex.Models.Checkpoint", b =>
                {
                    b.HasOne("Kubex.Models.CheckpointType", "CheckpointType")
                        .WithMany()
                        .HasForeignKey("CheckpointTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Kubex.Models.Landmap", null)
                        .WithMany("Checkpoints")
                        .HasForeignKey("LandmapId");

                    b.HasOne("Kubex.Models.Location", "Location")
                        .WithMany()
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Kubex.Models.Company", b =>
                {
                    b.HasOne("Kubex.Models.Address", "Address")
                        .WithMany("Companies")
                        .HasForeignKey("AddressId");
                });

            modelBuilder.Entity("Kubex.Models.Contact", b =>
                {
                    b.HasOne("Kubex.Models.Company", "Company")
                        .WithMany("Contacts")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Kubex.Models.ContactType", "ContactType")
                        .WithMany()
                        .HasForeignKey("ContactTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Kubex.Models.Post", null)
                        .WithMany("Contacts")
                        .HasForeignKey("PostId");

                    b.HasOne("Kubex.Models.User", null)
                        .WithMany("Contacts")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Kubex.Models.Entry", b =>
                {
                    b.HasOne("Kubex.Models.DailyActivityReport", "DailyActivityReport")
                        .WithMany("Entries")
                        .HasForeignKey("DailyActivityReportId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Kubex.Models.EntryType", "EntryType")
                        .WithMany()
                        .HasForeignKey("EntryTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Kubex.Models.Location", "Location")
                        .WithMany()
                        .HasForeignKey("LocationId");

                    b.HasOne("Kubex.Models.Entry", "ParentEntry")
                        .WithMany("ChildEntries")
                        .HasForeignKey("ParentEntryId");

                    b.HasOne("Kubex.Models.Priority", "Priority")
                        .WithMany()
                        .HasForeignKey("PriorityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Kubex.Models.License", b =>
                {
                    b.HasOne("Kubex.Models.LicenseType", "LicenseType")
                        .WithMany()
                        .HasForeignKey("LicenseTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Kubex.Models.User", "User")
                        .WithMany("Licenses")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Kubex.Models.Media", b =>
                {
                    b.HasOne("Kubex.Models.Entry", null)
                        .WithMany("Media")
                        .HasForeignKey("EntryId");

                    b.HasOne("Kubex.Models.EntryType", "EntryType")
                        .WithMany()
                        .HasForeignKey("EntryTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Kubex.Models.MediaType", "MediaType")
                        .WithMany()
                        .HasForeignKey("MediaTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Kubex.Models.Mission", b =>
                {
                    b.HasOne("Kubex.Models.Checkpoint", "Checkpoint")
                        .WithMany("Missions")
                        .HasForeignKey("CheckpointId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Kubex.Models.MissionType", "MissionType")
                        .WithMany()
                        .HasForeignKey("MissionTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Kubex.Models.Round", "Round")
                        .WithMany("Missions")
                        .HasForeignKey("RoundId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("Kubex.Models.Post", b =>
                {
                    b.HasOne("Kubex.Models.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Kubex.Models.Customer", "Customer")
                        .WithMany("Posts")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Kubex.Models.Location", "Location")
                        .WithMany()
                        .HasForeignKey("LocationId");
                });

            modelBuilder.Entity("Kubex.Models.Round", b =>
                {
                    b.HasOne("Kubex.Models.Entry", null)
                        .WithMany("Rounds")
                        .HasForeignKey("EntryId");

                    b.HasOne("Kubex.Models.Landmap", "Landmap")
                        .WithMany()
                        .HasForeignKey("LandmapId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Kubex.Models.Post", "Post")
                        .WithMany("Rounds")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Kubex.Models.RoundStatus", "RoundStatus")
                        .WithMany()
                        .HasForeignKey("RoundStatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Kubex.Models.User", b =>
                {
                    b.HasOne("Kubex.Models.Address", "Address")
                        .WithMany("Users")
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Kubex.Models.UserPost", b =>
                {
                    b.HasOne("Kubex.Models.Post", "Post")
                        .WithMany("UserPosts")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Kubex.Models.User", "User")
                        .WithMany("UserPosts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Kubex.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Kubex.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Kubex.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Kubex.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
