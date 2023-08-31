using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CVU.CONDICA.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Add_RequestedAt_to_Vacations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "RequestedAt",
                table: "Vacations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RequestedAt",
                table: "Vacations");
        }
    }
}
