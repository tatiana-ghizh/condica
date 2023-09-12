using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CVU.CONDICA.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Add_Columns_To_DepartmentRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "DepartmentId",
                table: "DepartmentRoles",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserCount",
                table: "DepartmentRoles",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserCount",
                table: "DepartmentRoles");

            migrationBuilder.AlterColumn<int>(
                name: "DepartmentId",
                table: "DepartmentRoles",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
