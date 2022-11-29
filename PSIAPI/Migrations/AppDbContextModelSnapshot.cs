﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PSIAPI.Data;

#nullable disable

namespace PSIAPI.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.0");

            modelBuilder.Entity("PSIAPI.Models.LocationItem", b =>
                {
                    b.Property<string>("ID")
                        .HasColumnType("TEXT");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<double>("Latitude")
                        .HasColumnType("REAL");

                    b.Property<double>("Longitude")
                        .HasColumnType("REAL");

                    b.Property<int>("State")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.ToTable("LocationItems");
                });

            modelBuilder.Entity("PSIAPI.Models.LogItem", b =>
                {
                    b.Property<string>("ID")
                        .HasColumnType("TEXT");

                    b.Property<string>("Date")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Details")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Trace")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.ToTable("LogItems");
                });
#pragma warning restore 612, 618
        }
    }
}