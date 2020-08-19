﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Pharmacy2URyanMoir.Data;

namespace Pharmacy2URyanMoir.Migrations
{
    [DbContext(typeof(Pharmacy2UContext))]
    [Migration("20200819192657_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Pharmacy2URyanMoir.Models.Currencies", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Exponent")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("ntext");

                    b.Property<string>("Symbol")
                        .IsRequired()
                        .HasColumnType("nvarchar(10)")
                        .HasMaxLength(10);

                    b.HasKey("Id");

                    b.ToTable("Currencies");
                });

            modelBuilder.Entity("Pharmacy2URyanMoir.Models.ExchangeRates", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BaseCurrecncy")
                        .HasColumnType("int");

                    b.Property<int>("ConvertedCurrency")
                        .HasColumnType("int");

                    b.Property<float>("ExchangeRate")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.ToTable("ExchangeRates");
                });

            modelBuilder.Entity("Pharmacy2URyanMoir.Models.Logs", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime");

                    b.Property<int>("ExchangeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Logs");
                });
#pragma warning restore 612, 618
        }
    }
}