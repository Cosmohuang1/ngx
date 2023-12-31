﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Stock.EntityFrameWork;

namespace Stock.EntityFrameWork.Migrations
{
    [DbContext(typeof(StockDBContext))]
    [Migration("20210510090002_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("Stock.EntityFrameWork.Model.AbuQuantModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("BS")
                        .HasColumnType("int");

                    b.Property<string>("Code")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("FW")
                        .HasColumnType("int");

                    b.Property<int>("KC")
                        .HasColumnType("int");

                    b.Property<int>("MC")
                        .HasColumnType("int");

                    b.Property<int>("MP")
                        .HasColumnType("int");

                    b.Property<int>("Rank")
                        .HasColumnType("int");

                    b.Property<int>("Score")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("AbuQuantModel");
                });

            modelBuilder.Entity("Stock.EntityFrameWork.Model.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .HasColumnType("longtext");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("longtext");

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("longtext");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("longtext");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UserName")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("ApplicationUser");
                });

            modelBuilder.Entity("Stock.EntityFrameWork.Model.Board", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("BoardCategory")
                        .HasColumnType("int");

                    b.Property<int>("BrokerSource")
                        .HasColumnType("int");

                    b.Property<string>("CagegoryId")
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Board");
                });

            modelBuilder.Entity("Stock.EntityFrameWork.Model.BoardToStocks", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("CagegoryId")
                        .HasColumnType("longtext");

                    b.Property<string>("StockCode")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("BoardToStocks");
                });

            modelBuilder.Entity("Stock.EntityFrameWork.Model.CustomCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreateDateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsActived")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("varchar(16)");

                    b.Property<DateTime>("UpdateDateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("CustomCategory");
                });

            modelBuilder.Entity("Stock.EntityFrameWork.Model.OptionalPool", b =>
                {
                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Code")
                        .HasMaxLength(6)
                        .HasColumnType("varchar(6)");

                    b.Property<DateTime>("CreateDateTime")
                        .HasColumnType("datetime(6)");

                    b.HasKey("CategoryId", "Code");

                    b.ToTable("OptionalPool");
                });

            modelBuilder.Entity("Stock.EntityFrameWork.Model.StockComment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ChangePercent")
                        .HasColumnType("longtext");

                    b.Property<string>("Code")
                        .HasColumnType("longtext");

                    b.Property<string>("FLOWINL")
                        .HasColumnType("longtext");

                    b.Property<string>("FLOWINXL")
                        .HasColumnType("longtext");

                    b.Property<string>("FLOWOUTL")
                        .HasColumnType("longtext");

                    b.Property<string>("FLOWOUTXL")
                        .HasColumnType("longtext");

                    b.Property<string>("Focus")
                        .HasColumnType("longtext");

                    b.Property<string>("JGCYD")
                        .HasColumnType("longtext");

                    b.Property<string>("JGCYDType")
                        .HasColumnType("longtext");

                    b.Property<string>("Market")
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<string>("PERation")
                        .HasColumnType("longtext");

                    b.Property<string>("Ranking")
                        .HasColumnType("longtext");

                    b.Property<string>("RankingUp")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("TDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("TotalScore")
                        .HasColumnType("longtext");

                    b.Property<string>("TurnoverRate")
                        .HasColumnType("longtext");

                    b.Property<string>("ZLCB")
                        .HasColumnType("longtext");

                    b.Property<string>("ZLCB20R")
                        .HasColumnType("longtext");

                    b.Property<string>("ZLCB60R")
                        .HasColumnType("longtext");

                    b.Property<string>("ZLJLR")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("StockComment");
                });

            modelBuilder.Entity("Stock.EntityFrameWork.Model.StockEntity", b =>
                {
                    b.Property<string>("Code")
                        .HasMaxLength(6)
                        .HasColumnType("varchar(6)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("varchar(16)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Code");

                    b.ToTable("StockEntity");
                });
#pragma warning restore 612, 618
        }
    }
}
