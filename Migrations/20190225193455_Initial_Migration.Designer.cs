﻿// <auto-generated />
using System;
using BackEnd_zadatak.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BackEnd_zadatak.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20190225193455_Initial_Migration")]
    partial class Initial_Migration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.1-servicing-10028")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BackEnd_zadatak.Models.Device", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("DeviceTypeId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("DeviceTypeId");

                    b.ToTable("Devices");
                });

            modelBuilder.Entity("BackEnd_zadatak.Models.DevicePropertyValue", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("DeviceId");

                    b.Property<int?>("DeviceTypePropertyId");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DeviceId");

                    b.HasIndex("DeviceTypePropertyId");

                    b.ToTable("DevicePropertyValues");
                });

            modelBuilder.Entity("BackEnd_zadatak.Models.DeviceType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)");

                    b.Property<int?>("ParentId");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.ToTable("DeviceTypes");
                });

            modelBuilder.Entity("BackEnd_zadatak.Models.DeviceTypeProperty", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("DeviceTypeId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("DeviceTypeId");

                    b.ToTable("DeviceTypeProperties");
                });

            modelBuilder.Entity("BackEnd_zadatak.Models.Device", b =>
                {
                    b.HasOne("BackEnd_zadatak.Models.DeviceType", "DeviceType")
                        .WithMany()
                        .HasForeignKey("DeviceTypeId");
                });

            modelBuilder.Entity("BackEnd_zadatak.Models.DevicePropertyValue", b =>
                {
                    b.HasOne("BackEnd_zadatak.Models.Device", "Device")
                        .WithMany("DevicePropertyValues")
                        .HasForeignKey("DeviceId");

                    b.HasOne("BackEnd_zadatak.Models.DeviceTypeProperty", "DeviceTypeProperty")
                        .WithMany()
                        .HasForeignKey("DeviceTypePropertyId");
                });

            modelBuilder.Entity("BackEnd_zadatak.Models.DeviceType", b =>
                {
                    b.HasOne("BackEnd_zadatak.Models.DeviceType", "ParentDeviceType")
                        .WithMany("ChildrenDeviceType")
                        .HasForeignKey("ParentId");
                });

            modelBuilder.Entity("BackEnd_zadatak.Models.DeviceTypeProperty", b =>
                {
                    b.HasOne("BackEnd_zadatak.Models.DeviceType", "DeviceType")
                        .WithMany("DeviceTypeProperty")
                        .HasForeignKey("DeviceTypeId");
                });
#pragma warning restore 612, 618
        }
    }
}
