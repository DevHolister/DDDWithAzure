﻿// <auto-generated />
using System;
using Linde.Persistence.Coaching;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Linde.Persistence.Coaching.Migrations
{
    [DbContext(typeof(CoachingDbContext))]
    [Migration("20231027190901_Init_Migration")]
    partial class Init_Migration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.23")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Linde.Domain.Coaching.CountryAggregate.Country", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("CountryId");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("ModifiedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("OriginalCode")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("Visible")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("CAT_COUNTRY", "dbo");
                });

            modelBuilder.Entity("Linde.Domain.Coaching.PermissionAggregate.Permission", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("PermissionId");

                    b.Property<string>("Actions")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.HasKey("Id");

                    b.ToTable("CAT_PERMISSION", "dbo");
                });

            modelBuilder.Entity("Linde.Domain.Coaching.RoleAggregate.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("RoleId");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("ModifiedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool>("Visible")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("CAT_ROLE", "dbo");
                });

            modelBuilder.Entity("Linde.Domain.Coaching.UserAggreagate.User", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("UserId");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Lockout")
                        .HasColumnType("bit");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("ModifiedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("Visible")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("UserName")
                        .IsUnique();

                    b.ToTable("CAT_USER", "dbo");
                });

            modelBuilder.Entity("Linde.Domain.Coaching.RoleAggregate.Role", b =>
                {
                    b.OwnsMany("Linde.Domain.Coaching.RoleAggregate.Entities.RolePermission", "PermissionItems", b1 =>
                        {
                            b1.Property<Guid>("RoleId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<Guid>("PermissionId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<DateTime>("CreatedAt")
                                .HasColumnType("datetime2");

                            b1.Property<Guid>("CreatedBy")
                                .HasColumnType("uniqueidentifier");

                            b1.HasKey("RoleId", "PermissionId");

                            b1.HasIndex("PermissionId");

                            b1.ToTable("ASOC_ROLE_PERMISSION", "dbo");

                            b1.HasOne("Linde.Domain.Coaching.PermissionAggregate.Permission", "Permission")
                                .WithMany()
                                .HasForeignKey("PermissionId")
                                .OnDelete(DeleteBehavior.Cascade)
                                .IsRequired();

                            b1.WithOwner()
                                .HasForeignKey("RoleId");

                            b1.Navigation("Permission");
                        });

                    b.Navigation("PermissionItems");
                });

            modelBuilder.Entity("Linde.Domain.Coaching.UserAggreagate.User", b =>
                {
                    b.OwnsMany("Linde.Domain.Coaching.UserAggreagate.Entities.UserCountry", "CountryItems", b1 =>
                        {
                            b1.Property<Guid>("UserId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<Guid>("CountryId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<DateTime>("CreatedAt")
                                .HasColumnType("datetime2");

                            b1.Property<Guid>("CreatedBy")
                                .HasColumnType("uniqueidentifier");

                            b1.HasKey("UserId", "CountryId");

                            b1.HasIndex("CountryId");

                            b1.ToTable("ASOC_USER_COUNTRY", "dbo");

                            b1.HasOne("Linde.Domain.Coaching.CountryAggregate.Country", "Country")
                                .WithMany()
                                .HasForeignKey("CountryId")
                                .OnDelete(DeleteBehavior.Cascade)
                                .IsRequired();

                            b1.WithOwner()
                                .HasForeignKey("UserId");

                            b1.Navigation("Country");
                        });

                    b.OwnsMany("Linde.Domain.Coaching.UserAggreagate.Entities.UserRole", "RoleItems", b1 =>
                        {
                            b1.Property<Guid>("UserId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<Guid>("RoleId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<DateTime>("CreatedAt")
                                .HasColumnType("datetime2");

                            b1.Property<Guid>("CreatedBy")
                                .HasColumnType("uniqueidentifier");

                            b1.HasKey("UserId", "RoleId");

                            b1.HasIndex("RoleId");

                            b1.ToTable("ASOC_USER_ROLE", "dbo");

                            b1.HasOne("Linde.Domain.Coaching.RoleAggregate.Role", "Role")
                                .WithMany()
                                .HasForeignKey("RoleId")
                                .OnDelete(DeleteBehavior.Cascade)
                                .IsRequired();

                            b1.WithOwner()
                                .HasForeignKey("UserId");

                            b1.Navigation("Role");
                        });

                    b.OwnsOne("Linde.Domain.Coaching.UserAggreagate.ValueObjects.FullName", "Name", b1 =>
                        {
                            b1.Property<Guid>("UserId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("FirstSurname")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("nvarchar(100)")
                                .HasColumnName("FirstSurname");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("nvarchar(100)")
                                .HasColumnName("Name");

                            b1.Property<string>("SecondSurname")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("nvarchar(100)")
                                .HasColumnName("SecondSurname");

                            b1.HasKey("UserId");

                            b1.ToTable("CAT_USER", "dbo");

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.Navigation("CountryItems");

                    b.Navigation("Name")
                        .IsRequired();

                    b.Navigation("RoleItems");
                });
#pragma warning restore 612, 618
        }
    }
}
