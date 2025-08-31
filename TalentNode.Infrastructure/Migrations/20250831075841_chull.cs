using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TalentNode.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class chull : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmployeeID",
                table: "Document");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EmployeeID",
                table: "Document",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
