﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SacramentMeetingPlanner.Data;

namespace SacramentMeetingPlanner.Data.Migrations
{
    [DbContext(typeof(SacramentMeetingPlannerContext))]
    [Migration("20190403144548_DataModelFirstDraft")]
    partial class DataModelFirstDraft
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.2-servicing-10034")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SacramentMeetingPlanner.Models.Meeting", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Benediction")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<string>("ClosingSong")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<string>("Conducting")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<string>("IntermediateSong")
                        .HasMaxLength(255);

                    b.Property<string>("Invocation")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<DateTime>("MeetingDate");

                    b.Property<string>("OpeningSong")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<string>("SacramentSong")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.HasKey("ID");

                    b.ToTable("Meeting");
                });

            modelBuilder.Entity("SacramentMeetingPlanner.Models.Setting", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("ID");

                    b.ToTable("Setting");
                });

            modelBuilder.Entity("SacramentMeetingPlanner.Models.Speaker", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Block");

                    b.Property<int>("MeetingID");

                    b.Property<int>("Order");

                    b.Property<string>("SpeakerName")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<string>("Subject")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.HasKey("ID");

                    b.HasIndex("MeetingID");

                    b.ToTable("Speaker");
                });

            modelBuilder.Entity("SacramentMeetingPlanner.Models.Speaker", b =>
                {
                    b.HasOne("SacramentMeetingPlanner.Models.Meeting", "Meeting")
                        .WithMany("Speakers")
                        .HasForeignKey("MeetingID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
