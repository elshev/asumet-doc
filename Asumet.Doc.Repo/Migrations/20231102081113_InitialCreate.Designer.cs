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
    [Migration("20231102081113_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Asumet.Entities.Buyer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Account")
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<string>("Address")
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<string>("Bank")
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<string>("Bic")
                        .HasMaxLength(9)
                        .HasColumnType("character varying(9)");

                    b.Property<string>("CorrespondentAccount")
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Inn")
                        .HasMaxLength(12)
                        .HasColumnType("character varying(12)");

                    b.Property<string>("Kpp")
                        .HasMaxLength(9)
                        .HasColumnType("character varying(9)");

                    b.Property<string>("ResponsiblePerson")
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.HasKey("Id");

                    b.ToTable("Buyers");
                });

            modelBuilder.Entity("Asumet.Entities.Psa", b =>
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
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<int>("BuyerId")
                        .HasColumnType("integer");

                    b.Property<string>("OwnershipReason")
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<string>("ShortScrapDescription")
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)");

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
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.HasKey("Id");

                    b.HasIndex("BuyerId");

                    b.HasIndex("SupplierId");

                    b.ToTable("Psas");
                });

            modelBuilder.Entity("Asumet.Entities.PsaScrap", b =>
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
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<decimal>("NetWeight")
                        .HasColumnType("numeric");

                    b.Property<decimal>("NonmetallicMixtures")
                        .HasColumnType("numeric");

                    b.Property<string>("Okpo")
                        .IsRequired()
                        .HasMaxLength(14)
                        .HasColumnType("character varying(14)");

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

            modelBuilder.Entity("Asumet.Entities.Supplier", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<string>("Passport")
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.HasKey("Id");

                    b.ToTable("Suppliers");
                });

            modelBuilder.Entity("Asumet.Entities.Psa", b =>
                {
                    b.HasOne("Asumet.Entities.Buyer", "Buyer")
                        .WithMany()
                        .HasForeignKey("BuyerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Asumet.Entities.Supplier", "Supplier")
                        .WithMany()
                        .HasForeignKey("SupplierId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Buyer");

                    b.Navigation("Supplier");
                });

            modelBuilder.Entity("Asumet.Entities.PsaScrap", b =>
                {
                    b.HasOne("Asumet.Entities.Psa", "Psa")
                        .WithMany("PsaScraps")
                        .HasForeignKey("PsaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Psa");
                });

            modelBuilder.Entity("Asumet.Entities.Psa", b =>
                {
                    b.Navigation("PsaScraps");
                });
#pragma warning restore 612, 618
        }
    }
}