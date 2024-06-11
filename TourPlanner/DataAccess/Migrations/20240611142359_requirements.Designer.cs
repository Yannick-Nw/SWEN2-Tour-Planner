﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using TourPlanner.DataAccess;

#nullable disable

namespace TourPlanner.Migrations
{
    [DbContext(typeof(TourPlannerContext))]
    [Migration("20240611142359_requirements")]
    partial class requirements
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("TourPlanner.BusinessLogic.Models.Tour", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("Distance")
                        .HasColumnType("double precision");

                    b.Property<TimeSpan>("EstimatedTime")
                        .HasColumnType("interval");

                    b.Property<string>("From")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Popularity")
                        .HasColumnType("integer");

                    b.Property<string>("To")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("TourImage")
                        .HasColumnType("text");

                    b.Property<int>("TransportType")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Tours");
                });

            modelBuilder.Entity("TourPlanner.BusinessLogic.Models.TourLog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Comment")
                        .HasColumnType("text");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Difficulty")
                        .HasColumnType("integer");

                    b.Property<int>("Rating")
                        .HasColumnType("integer");

                    b.Property<double>("TotalDistance")
                        .HasColumnType("double precision");

                    b.Property<TimeSpan>("TotalTime")
                        .HasColumnType("interval");

                    b.Property<int?>("TourId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("TourId");

                    b.ToTable("TourLogs");
                });

            modelBuilder.Entity("TourPlanner.BusinessLogic.Models.TourLog", b =>
                {
                    b.HasOne("TourPlanner.BusinessLogic.Models.Tour", null)
                        .WithMany("TourLogs")
                        .HasForeignKey("TourId");
                });

            modelBuilder.Entity("TourPlanner.BusinessLogic.Models.Tour", b =>
                {
                    b.Navigation("TourLogs");
                });
#pragma warning restore 612, 618
        }
    }
}
