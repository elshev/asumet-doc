﻿// <auto-generated />
using System;
using Asumet.Doc.Repo;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Asumet.Doc.Repo.Migrations
{
    [DbContext(typeof(DocDbContext))]
    [Migration("20231026092837_Init")]
    partial class Init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Asumet.Models.Buyer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Account")
                        .HasColumnType("text");

                    b.Property<string>("Address")
                        .HasColumnType("text");

                    b.Property<string>("Bank")
                        .HasColumnType("text");

                    b.Property<string>("Bic")
                        .HasColumnType("text");

                    b.Property<string>("CorrespondentAccount")
                        .HasColumnType("text");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Inn")
                        .HasColumnType("text");

                    b.Property<string>("Kpp")
                        .HasColumnType("text");

                    b.Property<string>("ResponsiblePerson")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Buyers");
                });

            modelBuilder.Entity("Asumet.Models.Psa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("ActDate")
                        .IsRequired()
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("ActNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("BuyerId")
                        .HasColumnType("integer");

                    b.Property<string>("OwnershipReason")
                        .HasColumnType("text");

                    b.Property<string>("ShortScrapDescription")
                        .HasColumnType("text");

                    b.Property<int>("SupplierId")
                        .HasColumnType("integer");

                    b.Property<decimal>("Total")
                        .HasColumnType("numeric");

                    b.Property<string>("TotalInWords")
                        .HasColumnType("text");

                    b.Property<decimal>("TotalNds")
                        .HasColumnType("numeric");

                    b.Property<string>("TotalNdsInWords")
                        .HasColumnType("text");

                    b.Property<decimal>("TotalNetto")
                        .HasColumnType("numeric");

                    b.Property<string>("TotalNettoInWords")
                        .HasColumnType("text");

                    b.Property<decimal>("TotalWoNds")
                        .HasColumnType("numeric");

                    b.Property<string>("Transport")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("BuyerId");

                    b.HasIndex("SupplierId");

                    b.ToTable("Psas");
                });

            modelBuilder.Entity("Asumet.Models.PsaScrap", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<decimal>("GrossWeight")
                        .HasColumnType("numeric");

                    b.Property<decimal>("MixturePercentage")
                        .HasColumnType("numeric");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("NetWeight")
                        .HasColumnType("numeric");

                    b.Property<decimal>("NonmetallicMixtures")
                        .HasColumnType("numeric");

                    b.Property<string>("Okpo")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.Property<int>("PsaId")
                        .HasColumnType("integer");

                    b.Property<decimal>("Sum")
                        .HasColumnType("numeric");

                    b.Property<decimal>("SumWoNds")
                        .HasColumnType("numeric");

                    b.Property<decimal>("TareWeight")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.HasIndex("PsaId");

                    b.ToTable("PsaScraps");
                });

            modelBuilder.Entity("Asumet.Models.Supplier", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .HasColumnType("text");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Passport")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Suppliers");
                });

            modelBuilder.Entity("Asumet.Models.Psa", b =>
                {
                    b.HasOne("Asumet.Models.Buyer", "Buyer")
                        .WithMany()
                        .HasForeignKey("BuyerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Asumet.Models.Supplier", "Supplier")
                        .WithMany()
                        .HasForeignKey("SupplierId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Buyer");

                    b.Navigation("Supplier");
                });

            modelBuilder.Entity("Asumet.Models.PsaScrap", b =>
                {
                    b.HasOne("Asumet.Models.Psa", "Psa")
                        .WithMany("PsaScraps")
                        .HasForeignKey("PsaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Psa");
                });

            modelBuilder.Entity("Asumet.Models.Psa", b =>
                {
                    b.Navigation("PsaScraps");
                });
#pragma warning restore 612, 618
        }
    }
}