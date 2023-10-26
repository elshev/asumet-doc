using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Asumet.Doc.Repo.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Buyers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FullName = table.Column<string>(type: "text", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: true),
                    Inn = table.Column<string>(type: "text", nullable: true),
                    Kpp = table.Column<string>(type: "text", nullable: true),
                    Bank = table.Column<string>(type: "text", nullable: true),
                    Bic = table.Column<string>(type: "text", nullable: true),
                    CorrespondentAccount = table.Column<string>(type: "text", nullable: true),
                    Account = table.Column<string>(type: "text", nullable: true),
                    ResponsiblePerson = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Buyers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FullName = table.Column<string>(type: "text", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: true),
                    Passport = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Psas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ActNumber = table.Column<string>(type: "text", nullable: false),
                    ActDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    BuyerId = table.Column<int>(type: "integer", nullable: false),
                    SupplierId = table.Column<int>(type: "integer", nullable: false),
                    ShortScrapDescription = table.Column<string>(type: "text", nullable: true),
                    OwnershipReason = table.Column<string>(type: "text", nullable: true),
                    Transport = table.Column<string>(type: "text", nullable: true),
                    TotalNetto = table.Column<decimal>(type: "numeric", nullable: false),
                    TotalNettoInWords = table.Column<string>(type: "text", nullable: true),
                    Total = table.Column<decimal>(type: "numeric", nullable: false),
                    TotalInWords = table.Column<string>(type: "text", nullable: true),
                    TotalWoNds = table.Column<decimal>(type: "numeric", nullable: false),
                    TotalNds = table.Column<decimal>(type: "numeric", nullable: false),
                    TotalNdsInWords = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Psas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Psas_Buyers_BuyerId",
                        column: x => x.BuyerId,
                        principalTable: "Buyers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Psas_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PsaScraps",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Okpo = table.Column<string>(type: "text", nullable: false),
                    PsaId = table.Column<int>(type: "integer", nullable: false),
                    GrossWeight = table.Column<decimal>(type: "numeric", nullable: false),
                    TareWeight = table.Column<decimal>(type: "numeric", nullable: false),
                    NonmetallicMixtures = table.Column<decimal>(type: "numeric", nullable: false),
                    MixturePercentage = table.Column<decimal>(type: "numeric", nullable: false),
                    NetWeight = table.Column<decimal>(type: "numeric", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    SumWoNds = table.Column<decimal>(type: "numeric", nullable: false),
                    Sum = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PsaScraps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PsaScraps_Psas_PsaId",
                        column: x => x.PsaId,
                        principalTable: "Psas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Psas_BuyerId",
                table: "Psas",
                column: "BuyerId");

            migrationBuilder.CreateIndex(
                name: "IX_Psas_SupplierId",
                table: "Psas",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_PsaScraps_PsaId",
                table: "PsaScraps",
                column: "PsaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PsaScraps");

            migrationBuilder.DropTable(
                name: "Psas");

            migrationBuilder.DropTable(
                name: "Buyers");

            migrationBuilder.DropTable(
                name: "Suppliers");
        }
    }
}
