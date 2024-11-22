﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Projectfinal.Model;

#nullable disable

namespace Projectfinal.Migrations
{
    [DbContext(typeof(dbcontext))]
    [Migration("20241027102016_EditOrdLone")]
    partial class EditOrdLone
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.10");

            modelBuilder.Entity("Projectfinal.Model.AdminRegisterModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Fullname")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Idcard")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Position")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Time")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("AdminRegisters");
                });

            modelBuilder.Entity("Projectfinal.Model.MoneyTrans", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Family")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Fullname")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<decimal>("MoneyLast")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("MoneyOld")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("MoneyTotal")
                        .HasColumnType("TEXT");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("TimeMoney")
                        .HasColumnType("TEXT");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("MoneyTranss");
                });

            modelBuilder.Entity("Projectfinal.Model.OrdLone", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Family")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Fullname")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Fullname1")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Fullname2")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Fullname3")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Interrate")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<decimal>("LoneMoney")
                        .HasColumnType("TEXT");

                    b.Property<int>("LoneMoney1")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("MoneyOld")
                        .HasColumnType("TEXT");

                    b.Property<string>("NumberLone")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Phone1")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Phone2")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Phone3")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("TimeLone")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("TotalMoneyLone")
                        .HasColumnType("TEXT");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Username1")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Username2")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Username3")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("OrdLones");
                });

            modelBuilder.Entity("Projectfinal.Model.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Family")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Fullname")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("IdCard")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneUser1")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneUser2")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Prefix")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("User1")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("User2")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
