using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RenergeIA.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ControlVersionesInformeDiario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Version",
                table: "InformesDiarios",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "ComentarioCambio",
                table: "InformesDiarios",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ComentarioCambio",
                table: "InformesDiarios");

            migrationBuilder.AlterColumn<int>(
                name: "Version",
                table: "InformesDiarios",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
