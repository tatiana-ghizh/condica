using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CVU.CONDICA.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Add_VacationDays : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NumberOfDays",
                table: "Vacations",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberOfDays",
                table: "Vacations");
        }
    }
}
