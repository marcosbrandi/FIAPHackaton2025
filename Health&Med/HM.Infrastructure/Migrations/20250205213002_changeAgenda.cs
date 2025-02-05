using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HM.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class changeAgenda : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataHora",
                table: "agendas");

            migrationBuilder.AddColumn<DateOnly>(
                name: "DataConsulta",
                table: "agendas",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<TimeOnly>(
                name: "FimConsulta",
                table: "agendas",
                type: "time",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));

            migrationBuilder.AddColumn<TimeOnly>(
                name: "InicioConsulta",
                table: "agendas",
                type: "time",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataConsulta",
                table: "agendas");

            migrationBuilder.DropColumn(
                name: "FimConsulta",
                table: "agendas");

            migrationBuilder.DropColumn(
                name: "InicioConsulta",
                table: "agendas");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataHora",
                table: "agendas",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
