﻿// <auto-generated />
using System;
using KonyvtarSzerver.Api;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace KonyvtarSzerver.Api.Migrations
{
    [DbContext(typeof(KonyvtarSzerverContext))]
    [Migration("20230503162442_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Konyvtar.Contracts.Konyv", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Cim")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("KiadasEve")
                        .HasColumnType("datetime2");

                    b.Property<string>("Kiado")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("LeltariSzam")
                        .HasColumnType("int");

                    b.Property<string>("Szerzo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Konyv");
                });
#pragma warning restore 612, 618
        }
    }
}
