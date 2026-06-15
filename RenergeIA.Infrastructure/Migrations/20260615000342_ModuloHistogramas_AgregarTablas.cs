using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RenergeIA.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ModuloHistogramas_AgregarTablas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PlantillasHistogramas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CapacidadMW = table.Column<decimal>(type: "decimal(6,2)", nullable: false),
                    Tipo = table.Column<int>(type: "int", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlantillasHistogramas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ItemsHistograma",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlantillaHistogramaId = table.Column<int>(type: "int", nullable: false),
                    CodigoCargo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Cargo = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    NombreHistograma = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Actividad = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    TiempoMeses = table.Column<decimal>(type: "decimal(4,1)", nullable: false),
                    Mes1 = table.Column<decimal>(type: "decimal(4,1)", nullable: false),
                    Mes2 = table.Column<decimal>(type: "decimal(4,1)", nullable: false),
                    Mes3 = table.Column<decimal>(type: "decimal(4,1)", nullable: false),
                    Mes4 = table.Column<decimal>(type: "decimal(4,1)", nullable: false),
                    Mes5 = table.Column<decimal>(type: "decimal(4,1)", nullable: false),
                    Mes6 = table.Column<decimal>(type: "decimal(4,1)", nullable: false),
                    Mes7 = table.Column<decimal>(type: "decimal(4,1)", nullable: false),
                    Mes8 = table.Column<decimal>(type: "decimal(4,1)", nullable: false),
                    Mes9 = table.Column<decimal>(type: "decimal(4,1)", nullable: false),
                    Mes10 = table.Column<decimal>(type: "decimal(4,1)", nullable: false),
                    Mes11 = table.Column<decimal>(type: "decimal(4,1)", nullable: false),
                    Mes12 = table.Column<decimal>(type: "decimal(4,1)", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemsHistograma", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemsHistograma_PlantillasHistogramas_PlantillaHistogramaId",
                        column: x => x.PlantillaHistogramaId,
                        principalTable: "PlantillasHistogramas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ItemsHistograma_PlantillaHistogramaId",
                table: "ItemsHistograma",
                column: "PlantillaHistogramaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemsHistograma");

            migrationBuilder.DropTable(
                name: "PlantillasHistogramas");
        }
    }
}
