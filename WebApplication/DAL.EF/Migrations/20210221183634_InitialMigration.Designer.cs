﻿// <auto-generated />
using System;
using DAL.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DAL.EF.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20210221183634_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.3");

            modelBuilder.Entity("Domain.Car", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("AddedAt")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("CarModelId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CarModelId");

                    b.ToTable("Cars");
                });

            modelBuilder.Entity("Domain.CarAccess", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("CarAccessTypeId")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("CarId")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CarAccessTypeId");

                    b.HasIndex("CarId");

                    b.ToTable("CarAccesses");
                });

            modelBuilder.Entity("Domain.CarAccessType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("AccessLevel")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("CarAccessTypes");
                });

            modelBuilder.Entity("Domain.CarErrorCode", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<long>("CanData")
                        .HasColumnType("INTEGER");

                    b.Property<int>("CanId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DetectedAt")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("CarErrorCodes");
                });

            modelBuilder.Entity("Domain.CarMark", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("CarMarks");
                });

            modelBuilder.Entity("Domain.CarModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("CarTypeId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("ReleaseDate")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CarTypeId");

                    b.ToTable("CarModels");
                });

            modelBuilder.Entity("Domain.CarType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("CarMarkId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CarMarkId");

                    b.ToTable("CarTypes");
                });

            modelBuilder.Entity("Domain.GasRefill", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<float>("AmountRefilled")
                        .HasColumnType("REAL");

                    b.Property<Guid?>("CarId")
                        .HasColumnType("TEXT");

                    b.Property<float>("Cost")
                        .HasColumnType("REAL");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CarId");

                    b.ToTable("GasRefills");
                });

            modelBuilder.Entity("Domain.Track", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("CarId")
                        .HasColumnType("TEXT");

                    b.Property<float>("Distance")
                        .HasColumnType("REAL");

                    b.Property<DateTime>("EndTimestamp")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("StartTimestamp")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CarId");

                    b.ToTable("Tracks");
                });

            modelBuilder.Entity("Domain.TrackLocation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<float>("Accuracy")
                        .HasColumnType("REAL");

                    b.Property<float>("Elevation")
                        .HasColumnType("REAL");

                    b.Property<float>("ElevationAccuracy")
                        .HasColumnType("REAL");

                    b.Property<double>("Lat")
                        .HasColumnType("REAL");

                    b.Property<double>("Lng")
                        .HasColumnType("REAL");

                    b.Property<float>("Rpm")
                        .HasColumnType("REAL");

                    b.Property<float>("Speed")
                        .HasColumnType("REAL");

                    b.Property<Guid?>("TrackId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("TrackId");

                    b.ToTable("TrackLocations");
                });

            modelBuilder.Entity("Domain.Car", b =>
                {
                    b.HasOne("Domain.CarModel", "CarModel")
                        .WithMany()
                        .HasForeignKey("CarModelId");

                    b.Navigation("CarModel");
                });

            modelBuilder.Entity("Domain.CarAccess", b =>
                {
                    b.HasOne("Domain.CarAccessType", "CarAccessType")
                        .WithMany()
                        .HasForeignKey("CarAccessTypeId");

                    b.HasOne("Domain.Car", "Car")
                        .WithMany()
                        .HasForeignKey("CarId");

                    b.Navigation("Car");

                    b.Navigation("CarAccessType");
                });

            modelBuilder.Entity("Domain.CarModel", b =>
                {
                    b.HasOne("Domain.CarType", "CarType")
                        .WithMany()
                        .HasForeignKey("CarTypeId");

                    b.Navigation("CarType");
                });

            modelBuilder.Entity("Domain.CarType", b =>
                {
                    b.HasOne("Domain.CarMark", "CarMark")
                        .WithMany()
                        .HasForeignKey("CarMarkId");

                    b.Navigation("CarMark");
                });

            modelBuilder.Entity("Domain.GasRefill", b =>
                {
                    b.HasOne("Domain.Car", "Car")
                        .WithMany()
                        .HasForeignKey("CarId");

                    b.Navigation("Car");
                });

            modelBuilder.Entity("Domain.Track", b =>
                {
                    b.HasOne("Domain.Car", "Car")
                        .WithMany()
                        .HasForeignKey("CarId");

                    b.Navigation("Car");
                });

            modelBuilder.Entity("Domain.TrackLocation", b =>
                {
                    b.HasOne("Domain.Track", "Track")
                        .WithMany()
                        .HasForeignKey("TrackId");

                    b.Navigation("Track");
                });
#pragma warning restore 612, 618
        }
    }
}
