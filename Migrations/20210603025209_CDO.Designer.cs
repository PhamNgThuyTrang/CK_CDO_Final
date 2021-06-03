﻿// <auto-generated />
using System;
using CK_CDO_Final.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Oracle.EntityFrameworkCore.Metadata;

namespace CK_CDO_Final.Migrations
{
    [DbContext(typeof(OracleDbContext))]
    [Migration("20210603025209_CDO")]
    partial class CDO
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Oracle:ValueGenerationStrategy", OracleValueGenerationStrategy.IdentityColumn)
                .HasAnnotation("ProductVersion", "3.1.15")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            modelBuilder.Entity("CK_CDO_Final.Entities.Hnx", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)")
                        .HasAnnotation("Oracle:ValueGenerationStrategy", OracleValueGenerationStrategy.IdentityColumn);

                    b.Property<float>("GIADONGCUA")
                        .HasColumnType("BINARY_FLOAT");

                    b.Property<float>("GIAMOCUA")
                        .HasColumnType("BINARY_FLOAT");

                    b.Property<float>("GIASAN")
                        .HasColumnType("BINARY_FLOAT");

                    b.Property<float>("GIATRAN")
                        .HasColumnType("BINARY_FLOAT");

                    b.Property<long>("KHOILUONG")
                        .HasColumnType("NUMBER(19)");

                    b.Property<string>("MA")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(10)")
                        .HasMaxLength(10);

                    b.Property<DateTime>("NGAY")
                        .HasColumnType("TIMESTAMP(7)");

                    b.HasKey("ID");

                    b.ToTable("Hnx");
                });

            modelBuilder.Entity("CK_CDO_Final.Entities.Hose", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)")
                        .HasAnnotation("Oracle:ValueGenerationStrategy", OracleValueGenerationStrategy.IdentityColumn);

                    b.Property<float>("GIADONGCUA")
                        .HasColumnType("BINARY_FLOAT");

                    b.Property<float>("GIAMOCUA")
                        .HasColumnType("BINARY_FLOAT");

                    b.Property<float>("GIASAN")
                        .HasColumnType("BINARY_FLOAT");

                    b.Property<float>("GIATRAN")
                        .HasColumnType("BINARY_FLOAT");

                    b.Property<long>("KHOILUONG")
                        .HasColumnType("NUMBER(19)");

                    b.Property<string>("MA")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(10)")
                        .HasMaxLength(10);

                    b.Property<DateTime>("NGAY")
                        .HasColumnType("TIMESTAMP(7)");

                    b.HasKey("ID");

                    b.ToTable("Hose");
                });

            modelBuilder.Entity("CK_CDO_Final.Entities.Index", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)")
                        .HasAnnotation("Oracle:ValueGenerationStrategy", OracleValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CHISO")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(10)")
                        .HasMaxLength(10);

                    b.Property<float>("DONGCUA")
                        .HasColumnType("BINARY_FLOAT");

                    b.Property<long>("KHOILUONG")
                        .HasColumnType("NUMBER(19)");

                    b.Property<float>("MOCUA")
                        .HasColumnType("BINARY_FLOAT");

                    b.Property<DateTime>("NGAY")
                        .HasColumnType("TIMESTAMP(7)");

                    b.Property<float>("SAN")
                        .HasColumnType("BINARY_FLOAT");

                    b.Property<float>("TRAN")
                        .HasColumnType("BINARY_FLOAT");

                    b.HasKey("ID");

                    b.ToTable("Index");
                });

            modelBuilder.Entity("CK_CDO_Final.Entities.Upcom", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)")
                        .HasAnnotation("Oracle:ValueGenerationStrategy", OracleValueGenerationStrategy.IdentityColumn);

                    b.Property<float>("GIADONGCUA")
                        .HasColumnType("BINARY_FLOAT");

                    b.Property<float>("GIAMOCUA")
                        .HasColumnType("BINARY_FLOAT");

                    b.Property<float>("GIASAN")
                        .HasColumnType("BINARY_FLOAT");

                    b.Property<float>("GIATRAN")
                        .HasColumnType("BINARY_FLOAT");

                    b.Property<long>("KHOILUONG")
                        .HasColumnType("NUMBER(19)");

                    b.Property<string>("MA")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(10)")
                        .HasMaxLength(10);

                    b.Property<DateTime>("NGAY")
                        .HasColumnType("TIMESTAMP(7)");

                    b.HasKey("ID");

                    b.ToTable("Upcom");
                });

            modelBuilder.Entity("CK_CDO_Final.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("NVARCHAR2(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("NUMBER(10)");

                    b.Property<string>("Address")
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<DateTime?>("DOB")
                        .HasColumnType("TIMESTAMP(7)");

                    b.Property<string>("Email")
                        .HasColumnType("NVARCHAR2(256)")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("NUMBER(1)");

                    b.Property<string>("FirstName")
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<int>("Gender")
                        .HasColumnType("NUMBER(10)");

                    b.Property<string>("LastName")
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("NUMBER(1)");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("TIMESTAMP(7) WITH TIME ZONE");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("NVARCHAR2(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("NVARCHAR2(256)")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash")
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("NUMBER(1)");

                    b.Property<int>("Role")
                        .HasColumnType("NUMBER(10)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("NUMBER(1)");

                    b.Property<string>("UserName")
                        .HasColumnType("NVARCHAR2(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("NVARCHAR2(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<string>("Name")
                        .HasColumnType("NVARCHAR2(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasColumnType("NVARCHAR2(256)")
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
                        .HasColumnType("NUMBER(10)")
                        .HasAnnotation("Oracle:ValueGenerationStrategy", OracleValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)")
                        .HasAnnotation("Oracle:ValueGenerationStrategy", OracleValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("NVARCHAR2(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("NVARCHAR2(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("NVARCHAR2(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("NVARCHAR2(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("NVARCHAR2(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("NVARCHAR2(450)");

                    b.Property<string>("Name")
                        .HasColumnType("NVARCHAR2(450)");

                    b.Property<string>("Value")
                        .HasColumnType("NVARCHAR2(2000)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
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
                    b.HasOne("CK_CDO_Final.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("CK_CDO_Final.Models.ApplicationUser", null)
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

                    b.HasOne("CK_CDO_Final.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("CK_CDO_Final.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
