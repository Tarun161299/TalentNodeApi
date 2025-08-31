using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TalentNode.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class xxx : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EmpImageID",
                table: "Employee",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ResumeID",
                table: "Employee",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmpImageID",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "ResumeID",
                table: "Employee");
        }
    }
}
