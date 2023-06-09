﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyWebApiApp.Data;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace MyWebApiApp.Migrations
{
    [DbContext(typeof(MyDbContext))]
    [Migration("20230605173149_DbPortgreSQL")]
    partial class DbPortgreSQL
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("MyWebApiApp.Data.HangHoa", b =>
                {
                    b.Property<Guid>("IdHangHoa")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<double>("DonGia")
                        .HasColumnType("double precision");

                    b.Property<byte>("GiamGia")
                        .HasColumnType("smallint");

                    b.Property<Guid?>("IdLoai")
                        .HasColumnType("uuid");

                    b.Property<string>("MoTa")
                        .HasColumnType("text");

                    b.Property<string>("TenHangHoa")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)");

                    b.HasKey("IdHangHoa");

                    b.HasIndex("IdLoai");

                    b.ToTable("HangHoa");
                });

            modelBuilder.Entity("MyWebApiApp.Data.Loai", b =>
                {
                    b.Property<Guid>("IdLoai")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("TenLoai")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("IdLoai");

                    b.ToTable("Loai");
                });

            modelBuilder.Entity("MyWebApiApp.Data.HangHoa", b =>
                {
                    b.HasOne("MyWebApiApp.Data.Loai", "Loai")
                        .WithMany("HangHoas")
                        .HasForeignKey("IdLoai");

                    b.Navigation("Loai");
                });

            modelBuilder.Entity("MyWebApiApp.Data.Loai", b =>
                {
                    b.Navigation("HangHoas");
                });
#pragma warning restore 612, 618
        }
    }
}
