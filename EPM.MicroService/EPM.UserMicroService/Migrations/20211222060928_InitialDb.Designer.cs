﻿// <auto-generated />
using System;
using EPM.UserMicroService.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EPM.UserMicroService.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20211222060928_InitialDb")]
    partial class InitialDb
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EPM.Model.DbModel.User", b =>
                {
                    b.Property<int>("ClusterID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreateUser")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DepartmentName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EmailAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("ID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("IsDeleted")
                        .HasColumnType("int");

                    b.Property<int>("LoginErrorCount")
                        .HasColumnType("int");

                    b.Property<DateTime?>("LoginLockTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("LoginName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LoginTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("MobileNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("PasswordUpdateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Position")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdateUser")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ClusterID");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            ClusterID = 1,
                            CreateTime = new DateTime(2021, 12, 22, 14, 9, 28, 107, DateTimeKind.Local).AddTicks(7611),
                            CreateUser = "系统管理员",
                            DepartmentName = "技术中心",
                            ID = new Guid("316e60b8-89c2-4d33-a31c-8740c54a4867"),
                            IsDeleted = 0,
                            LoginErrorCount = 0,
                            LoginName = "admin",
                            Name = "系统管理员",
                            Password = "e10adc3949ba59abbe56e057f20f883e",
                            RoleName = "超级管理员",
                            Status = 0,
                            UpdateTime = new DateTime(2021, 12, 22, 14, 9, 28, 107, DateTimeKind.Local).AddTicks(8326),
                            UpdateUser = "系统管理员"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}