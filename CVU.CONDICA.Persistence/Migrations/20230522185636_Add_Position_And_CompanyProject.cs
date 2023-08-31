using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CVU.CONDICA.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Add_Position_And_CompanyProject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PositionId",
                table: "User",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "CompanyProjects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDay = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDay = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyProjects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Positions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Positions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserCompanyProjects",
                columns: table => new
                {
                    CompanyProjectId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCompanyProjects", x => new { x.UserId, x.CompanyProjectId });
                    table.ForeignKey(
                        name: "FK_UserCompanyProjects_CompanyProjects_CompanyProjectId",
                        column: x => x.CompanyProjectId,
                        principalTable: "CompanyProjects",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserCompanyProjects_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_User_PositionId",
                table: "User",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCompanyProjects_CompanyProjectId",
                table: "UserCompanyProjects",
                column: "CompanyProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Positions_PositionId",
                table: "User",
                column: "PositionId",
                principalTable: "Positions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Positions_PositionId",
                table: "User");

            migrationBuilder.DropTable(
                name: "Positions");

            migrationBuilder.DropTable(
                name: "UserCompanyProjects");

            migrationBuilder.DropTable(
                name: "CompanyProjects");

            migrationBuilder.DropIndex(
                name: "IX_User_PositionId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "PositionId",
                table: "User");
        }
    }
}
