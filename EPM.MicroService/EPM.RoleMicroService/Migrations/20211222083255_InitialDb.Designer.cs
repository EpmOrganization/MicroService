﻿// <auto-generated />
using System;
using EPM.RoleMicroService.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EPM.RoleMicroService.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20211222083255_InitialDb")]
    partial class InitialDb
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EPM.Model.DbModel.Role", b =>
                {
                    b.Property<int>("ClusterID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreateUser")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HalfCheckeds")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("ID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("IsDeleted")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdateUser")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ClusterID");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            ClusterID = 1,
                            CreateTime = new DateTime(2021, 12, 22, 16, 32, 54, 648, DateTimeKind.Local).AddTicks(1976),
                            CreateUser = "系统管理员",
                            ID = new Guid("d4c6bc89-d592-4ea6-94ff-0ebf9999892c"),
                            IsDeleted = 0,
                            Name = "系统管理员",
                            UpdateTime = new DateTime(2021, 12, 22, 16, 32, 54, 648, DateTimeKind.Local).AddTicks(2277),
                            UpdateUser = "系统管理员"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}