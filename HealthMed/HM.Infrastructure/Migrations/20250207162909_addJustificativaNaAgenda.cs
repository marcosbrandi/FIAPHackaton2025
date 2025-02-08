using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HM.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addJustificativaNaAgenda : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Justificativa",
                table: "agendas",
                type: "varchar(500)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Justificativa",
                table: "agendas");
        }
    }
}
