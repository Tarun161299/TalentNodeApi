using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TalentNode.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class NoticePeriod : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "iscurrentlyworking",
                table: "EmployeeProjects",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NoticePeriod",
                table: "Employee",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NoticePeriod",
                table: "Employee");

            migrationBuilder.AlterColumn<bool>(
                name: "iscurrentlyworking",
                table: "EmployeeProjects",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");
        }
    }
}
