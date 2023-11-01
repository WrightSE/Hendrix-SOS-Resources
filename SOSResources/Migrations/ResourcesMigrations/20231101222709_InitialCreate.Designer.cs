﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SOSResources.Data;

#nullable disable

namespace SOSResources.Migrations.ResourcesMigrations
{
    [DbContext(typeof(Resources))]
    [Migration("20231101222709_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.13");

            modelBuilder.Entity("ResourceResourceRequest", b =>
                {
                    b.Property<int>("ResourceRequestsID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ResourcesID")
                        .HasColumnType("INTEGER");

                    b.HasKey("ResourceRequestsID", "ResourcesID");

                    b.HasIndex("ResourcesID");

                    b.ToTable("ResourceResourceRequest");
                });

            modelBuilder.Entity("SOSResources.Models.Participant", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT")
                        .HasColumnName("FirstName");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.ToTable("Participant");
                });

            modelBuilder.Entity("SOSResources.Models.Resource", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<int>("Quantity")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.ToTable("Resource");
                });

            modelBuilder.Entity("SOSResources.Models.ResourceRequest", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ParticipantID")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("RequestDate")
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.HasIndex("ParticipantID");

                    b.ToTable("ResourceRequest");
                });

            modelBuilder.Entity("SOSResources.Models.Textbook", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("AuthorFirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT")
                        .HasColumnName("FirstName");

                    b.Property<string>("AuthorLastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<bool>("CheckedOut")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Edition")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<int>("Quantity")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.ToTable("Textbook");
                });

            modelBuilder.Entity("SOSResources.Models.TextbookRequest", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ParticipantID")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("RequestDate")
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.HasIndex("ParticipantID");

                    b.ToTable("TextbookRequest");
                });

            modelBuilder.Entity("TextbookTextbookRequest", b =>
                {
                    b.Property<int>("TextbookID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TextbookRequestsID")
                        .HasColumnType("INTEGER");

                    b.HasKey("TextbookID", "TextbookRequestsID");

                    b.HasIndex("TextbookRequestsID");

                    b.ToTable("TextbookTextbookRequest");
                });

            modelBuilder.Entity("ResourceResourceRequest", b =>
                {
                    b.HasOne("SOSResources.Models.ResourceRequest", null)
                        .WithMany()
                        .HasForeignKey("ResourceRequestsID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SOSResources.Models.Resource", null)
                        .WithMany()
                        .HasForeignKey("ResourcesID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SOSResources.Models.ResourceRequest", b =>
                {
                    b.HasOne("SOSResources.Models.Participant", "Participant")
                        .WithMany("ResourceRequests")
                        .HasForeignKey("ParticipantID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Participant");
                });

            modelBuilder.Entity("SOSResources.Models.TextbookRequest", b =>
                {
                    b.HasOne("SOSResources.Models.Participant", "Participant")
                        .WithMany("TextbookRequests")
                        .HasForeignKey("ParticipantID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Participant");
                });

            modelBuilder.Entity("TextbookTextbookRequest", b =>
                {
                    b.HasOne("SOSResources.Models.Textbook", null)
                        .WithMany()
                        .HasForeignKey("TextbookID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SOSResources.Models.TextbookRequest", null)
                        .WithMany()
                        .HasForeignKey("TextbookRequestsID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SOSResources.Models.Participant", b =>
                {
                    b.Navigation("ResourceRequests");

                    b.Navigation("TextbookRequests");
                });
#pragma warning restore 612, 618
        }
    }
}