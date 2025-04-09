﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ParkingFlow.Data;

#nullable disable

namespace ParkingFlow.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250409083325_ConfigureEntityRelationshipBookingsAndSlots")]
    partial class ConfigureEntityRelationshipBookingsAndSlots
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("ParkingFlow.Models.Bookings", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("BookingDate")
                        .HasColumnType("datetime2");

                    b.Property<TimeSpan>("EndTime")
                        .HasColumnType("time");

                    b.Property<int>("ParkingSlotId")
                        .HasColumnType("int");

                    b.Property<TimeSpan>("StartTime")
                        .HasColumnType("time");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ParkingSlotId");

                    b.ToTable("Bookings");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            BookingDate = new DateTime(2025, 4, 7, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EndTime = new TimeSpan(0, 10, 0, 0, 0),
                            ParkingSlotId = 1,
                            StartTime = new TimeSpan(0, 9, 0, 0, 0),
                            UserId = "test-user"
                        });
                });

            modelBuilder.Entity("ParkingFlow.Models.ParkingSlots", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsVacant")
                        .HasColumnType("bit");

                    b.Property<string>("Location")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("SlotCode")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.HasKey("Id");

                    b.ToTable("ParkingSlots");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            IsVacant = true,
                            Location = "Kensington Avenue",
                            SlotCode = "K1"
                        },
                        new
                        {
                            Id = 2,
                            IsVacant = true,
                            Location = "Kensington Avenue",
                            SlotCode = "K2"
                        },
                        new
                        {
                            Id = 3,
                            IsVacant = true,
                            Location = "Kensington Avenue",
                            SlotCode = "K3"
                        },
                        new
                        {
                            Id = 4,
                            IsVacant = true,
                            Location = "Kensington Avenue",
                            SlotCode = "K4"
                        },
                        new
                        {
                            Id = 5,
                            IsVacant = true,
                            Location = "Kensington Avenue",
                            SlotCode = "K5"
                        },
                        new
                        {
                            Id = 6,
                            IsVacant = true,
                            Location = "Kensington Avenue",
                            SlotCode = "K6"
                        },
                        new
                        {
                            Id = 7,
                            IsVacant = true,
                            Location = "Kensington Avenue",
                            SlotCode = "K7"
                        },
                        new
                        {
                            Id = 8,
                            IsVacant = true,
                            Location = "Kensington Avenue",
                            SlotCode = "K8"
                        },
                        new
                        {
                            Id = 9,
                            IsVacant = true,
                            Location = "Kensington Avenue",
                            SlotCode = "K9"
                        },
                        new
                        {
                            Id = 10,
                            IsVacant = true,
                            Location = "Kensington Avenue",
                            SlotCode = "K10"
                        },
                        new
                        {
                            Id = 11,
                            IsVacant = true,
                            Location = "Kensington Avenue",
                            SlotCode = "K11"
                        },
                        new
                        {
                            Id = 12,
                            IsVacant = true,
                            Location = "Kensington Avenue",
                            SlotCode = "K12"
                        },
                        new
                        {
                            Id = 13,
                            IsVacant = true,
                            Location = "Huia Street",
                            SlotCode = "H1"
                        },
                        new
                        {
                            Id = 14,
                            IsVacant = true,
                            Location = "Huia Street",
                            SlotCode = "H2"
                        },
                        new
                        {
                            Id = 15,
                            IsVacant = true,
                            Location = "Huia Street",
                            SlotCode = "H3"
                        },
                        new
                        {
                            Id = 16,
                            IsVacant = true,
                            Location = "Huia Street",
                            SlotCode = "H4"
                        },
                        new
                        {
                            Id = 17,
                            IsVacant = true,
                            Location = "Huia Street",
                            SlotCode = "H5"
                        },
                        new
                        {
                            Id = 18,
                            IsVacant = true,
                            Location = "Huia Street",
                            SlotCode = "H6"
                        },
                        new
                        {
                            Id = 19,
                            IsVacant = true,
                            Location = "Huia Street",
                            SlotCode = "H7"
                        },
                        new
                        {
                            Id = 20,
                            IsVacant = true,
                            Location = "Huia Street",
                            SlotCode = "H8"
                        },
                        new
                        {
                            Id = 21,
                            IsVacant = true,
                            Location = "Huia Street",
                            SlotCode = "H9"
                        },
                        new
                        {
                            Id = 22,
                            IsVacant = true,
                            Location = "Huia Street",
                            SlotCode = "H10"
                        },
                        new
                        {
                            Id = 23,
                            IsVacant = true,
                            Location = "Huia Street",
                            SlotCode = "H11"
                        },
                        new
                        {
                            Id = 24,
                            IsVacant = true,
                            Location = "Huia Street",
                            SlotCode = "H12"
                        },
                        new
                        {
                            Id = 25,
                            IsVacant = true,
                            Location = "Udy Street",
                            SlotCode = "U1"
                        },
                        new
                        {
                            Id = 26,
                            IsVacant = true,
                            Location = "Udy Street",
                            SlotCode = "U2"
                        },
                        new
                        {
                            Id = 27,
                            IsVacant = true,
                            Location = "Udy Street",
                            SlotCode = "U3"
                        },
                        new
                        {
                            Id = 28,
                            IsVacant = true,
                            Location = "Udy Street",
                            SlotCode = "U4"
                        },
                        new
                        {
                            Id = 29,
                            IsVacant = true,
                            Location = "Udy Street",
                            SlotCode = "U5"
                        },
                        new
                        {
                            Id = 30,
                            IsVacant = true,
                            Location = "Udy Street",
                            SlotCode = "U6"
                        },
                        new
                        {
                            Id = 31,
                            IsVacant = true,
                            Location = "Udy Street",
                            SlotCode = "U7"
                        },
                        new
                        {
                            Id = 32,
                            IsVacant = true,
                            Location = "Udy Street",
                            SlotCode = "U8"
                        },
                        new
                        {
                            Id = 33,
                            IsVacant = true,
                            Location = "Udy Street",
                            SlotCode = "U9"
                        },
                        new
                        {
                            Id = 34,
                            IsVacant = true,
                            Location = "Udy Street",
                            SlotCode = "U10"
                        },
                        new
                        {
                            Id = 35,
                            IsVacant = true,
                            Location = "Udy Street",
                            SlotCode = "U11"
                        },
                        new
                        {
                            Id = 36,
                            IsVacant = true,
                            Location = "Udy Street",
                            SlotCode = "U12"
                        },
                        new
                        {
                            Id = 37,
                            IsVacant = true,
                            Location = "Atiawa Street",
                            SlotCode = "A1"
                        },
                        new
                        {
                            Id = 38,
                            IsVacant = true,
                            Location = "Atiawa Street",
                            SlotCode = "A2"
                        },
                        new
                        {
                            Id = 39,
                            IsVacant = true,
                            Location = "Atiawa Street",
                            SlotCode = "A3"
                        },
                        new
                        {
                            Id = 40,
                            IsVacant = true,
                            Location = "Atiawa Street",
                            SlotCode = "A4"
                        },
                        new
                        {
                            Id = 41,
                            IsVacant = true,
                            Location = "Atiawa Street",
                            SlotCode = "A5"
                        },
                        new
                        {
                            Id = 42,
                            IsVacant = true,
                            Location = "Atiawa Street",
                            SlotCode = "A6"
                        },
                        new
                        {
                            Id = 43,
                            IsVacant = true,
                            Location = "Atiawa Street",
                            SlotCode = "A7"
                        },
                        new
                        {
                            Id = 44,
                            IsVacant = true,
                            Location = "Atiawa Street",
                            SlotCode = "A8"
                        },
                        new
                        {
                            Id = 45,
                            IsVacant = true,
                            Location = "Atiawa Street",
                            SlotCode = "A9"
                        },
                        new
                        {
                            Id = 46,
                            IsVacant = true,
                            Location = "Atiawa Street",
                            SlotCode = "A10"
                        },
                        new
                        {
                            Id = 47,
                            IsVacant = true,
                            Location = "Atiawa Street",
                            SlotCode = "A11"
                        },
                        new
                        {
                            Id = 48,
                            IsVacant = true,
                            Location = "Atiawa Street",
                            SlotCode = "A12"
                        });
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
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
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

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ParkingFlow.Models.Bookings", b =>
                {
                    b.HasOne("ParkingFlow.Models.ParkingSlots", "ParkingSlot")
                        .WithMany("Bookings")
                        .HasForeignKey("ParkingSlotId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("ParkingSlot");
                });

            modelBuilder.Entity("ParkingFlow.Models.ParkingSlots", b =>
                {
                    b.Navigation("Bookings");
                });
#pragma warning restore 612, 618
        }
    }
}
