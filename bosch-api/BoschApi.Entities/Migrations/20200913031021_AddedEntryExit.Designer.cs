﻿// <auto-generated />
using System;
using BoschApi.Entities.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BoschApi.Entities.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20200913031021_AddedEntryExit")]
    partial class AddedEntryExit
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.14-servicing-32113")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BoschApi.Entities.Data.Camera", b =>
                {
                    b.Property<int>("CameraId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AdditionalDetail");

                    b.Property<string>("CameraIP");

                    b.Property<string>("CameraName");

                    b.Property<int>("GateId");

                    b.Property<string>("Model");

                    b.HasKey("CameraId");

                    b.HasIndex("GateId");

                    b.ToTable("Cameras");
                });

            modelBuilder.Entity("BoschApi.Entities.Data.EntryRecord", b =>
                {
                    b.Property<int>("EntryRecordId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CameraId");

                    b.Property<DateTime>("Timestamp");

                    b.HasKey("EntryRecordId");

                    b.HasIndex("CameraId");

                    b.ToTable("EntryRecords");
                });

            modelBuilder.Entity("BoschApi.Entities.Data.ExitRecord", b =>
                {
                    b.Property<int>("ExitRecordId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CameraId");

                    b.Property<DateTime>("Timestamp");

                    b.HasKey("ExitRecordId");

                    b.HasIndex("CameraId");

                    b.ToTable("ExitRecords");
                });

            modelBuilder.Entity("BoschApi.Entities.Data.Gate", b =>
                {
                    b.Property<int>("GateId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("GateName");

                    b.Property<int>("SiteId");

                    b.HasKey("GateId");

                    b.HasIndex("SiteId");

                    b.ToTable("Gates");
                });

            modelBuilder.Entity("BoschApi.Entities.Data.Site", b =>
                {
                    b.Property<int>("SiteId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("SiteDescription");

                    b.Property<string>("SiteName");

                    b.HasKey("SiteId");

                    b.ToTable("Sites");
                });

            modelBuilder.Entity("BoschApi.Entities.Data.Camera", b =>
                {
                    b.HasOne("BoschApi.Entities.Data.Gate", "Gate")
                        .WithMany("Cameras")
                        .HasForeignKey("GateId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BoschApi.Entities.Data.EntryRecord", b =>
                {
                    b.HasOne("BoschApi.Entities.Data.Camera", "Camera")
                        .WithMany("EntryRecords")
                        .HasForeignKey("CameraId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BoschApi.Entities.Data.ExitRecord", b =>
                {
                    b.HasOne("BoschApi.Entities.Data.Camera", "Camera")
                        .WithMany("ExitRecords")
                        .HasForeignKey("CameraId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BoschApi.Entities.Data.Gate", b =>
                {
                    b.HasOne("BoschApi.Entities.Data.Site", "Site")
                        .WithMany("Gates")
                        .HasForeignKey("SiteId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
