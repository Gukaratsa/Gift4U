﻿// <auto-generated />
using System;
using Gift4U.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Gift4U.BlazorApp.Migrations
{
    [DbContext(typeof(GiftDBContext))]
    partial class GiftDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.1");

            modelBuilder.Entity("Gift4U.Domain.Models.Gift", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<TimeSpan?>("Duration")
                        .HasColumnType("TEXT");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Gifts");
                });

            modelBuilder.Entity("Gift4U.Domain.Models.GivenGift", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("Amount")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("GiftId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("GiverId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("ReceiverId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("GiftId");

                    b.HasIndex("GiverId");

                    b.HasIndex("ReceiverId");

                    b.ToTable("GivenGifts");
                });

            modelBuilder.Entity("Gift4U.Domain.Models.Request", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("GivenInRequestId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("RequestStateId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Requested")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("Responded")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("Started")
                        .HasColumnType("TEXT");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("GivenInRequestId");

                    b.HasIndex("RequestStateId");

                    b.ToTable("Requests");
                });

            modelBuilder.Entity("Gift4U.Domain.Models.RequestState", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("RequestStates");

                    b.HasData(
                        new
                        {
                            Id = new Guid("d4e4fb8f-cd98-4460-9213-8e9337f9f448"),
                            Name = "Pending"
                        },
                        new
                        {
                            Id = new Guid("894fd9e4-7705-4d92-8e81-aae37aae4318"),
                            Name = "RequestApproved"
                        },
                        new
                        {
                            Id = new Guid("0caf8b68-916d-4863-b528-b1d2b8954188"),
                            Name = "RequestDenied"
                        },
                        new
                        {
                            Id = new Guid("02ba5abd-e886-4191-97a8-4f6d3c1c683e"),
                            Name = "RequestStarted"
                        },
                        new
                        {
                            Id = new Guid("ba94bc1d-2a8e-4d6a-a293-883756e0398d"),
                            Name = "Completed"
                        });
                });

            modelBuilder.Entity("Gift4U.Domain.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsEmailConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Gift4U.Domain.Models.GivenGift", b =>
                {
                    b.HasOne("Gift4U.Domain.Models.Gift", "Gift")
                        .WithMany("GivenGifts")
                        .HasForeignKey("GiftId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Gift4U.Domain.Models.User", "Giver")
                        .WithMany("GivenGifts")
                        .HasForeignKey("GiverId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Gift4U.Domain.Models.User", "Receiver")
                        .WithMany("ReceivedGifts")
                        .HasForeignKey("ReceiverId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Gift");

                    b.Navigation("Giver");

                    b.Navigation("Receiver");
                });

            modelBuilder.Entity("Gift4U.Domain.Models.Request", b =>
                {
                    b.HasOne("Gift4U.Domain.Models.GivenGift", "GivenInRequest")
                        .WithMany("Requests")
                        .HasForeignKey("GivenInRequestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Gift4U.Domain.Models.RequestState", "RequestState")
                        .WithMany("Requests")
                        .HasForeignKey("RequestStateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("GivenInRequest");

                    b.Navigation("RequestState");
                });

            modelBuilder.Entity("Gift4U.Domain.Models.Gift", b =>
                {
                    b.Navigation("GivenGifts");
                });

            modelBuilder.Entity("Gift4U.Domain.Models.GivenGift", b =>
                {
                    b.Navigation("Requests");
                });

            modelBuilder.Entity("Gift4U.Domain.Models.RequestState", b =>
                {
                    b.Navigation("Requests");
                });

            modelBuilder.Entity("Gift4U.Domain.Models.User", b =>
                {
                    b.Navigation("GivenGifts");

                    b.Navigation("ReceivedGifts");
                });
#pragma warning restore 612, 618
        }
    }
}
