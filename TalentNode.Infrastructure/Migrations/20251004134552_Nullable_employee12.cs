using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TalentNode.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Nullable_employee12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeExperiences_Employee_EmployeeID",
                table: "EmployeeExperiences");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeExperiences_Experience_ExperienceID",
                table: "EmployeeExperiences");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmployeeExperiences",
                table: "EmployeeExperiences");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeExperiences_EmployeeID",
                table: "EmployeeExperiences");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeExperiences_ExperienceID",
                table: "EmployeeExperiences");

            migrationBuilder.DropColumn(
                name: "EmployeeExperienceID",
                table: "EmployeeExperiences");

            migrationBuilder.AddColumn<int>(
                name: "UserID",
                table: "Employee",
                type: "int",
                maxLength: 100,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmployeeExperiences",
                table: "EmployeeExperiences",
                columns: new[] { "EmployeeID", "ExperienceID" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_EmployeeExperiences",
                table: "EmployeeExperiences");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "Employee");

            migrationBuilder.AddColumn<int>(
                name: "EmployeeExperienceID",
                table: "EmployeeExperiences",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmployeeExperiences",
                table: "EmployeeExperiences",
                column: "EmployeeExperienceID");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeExperiences_EmployeeID",
                table: "EmployeeExperiences",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeExperiences_ExperienceID",
                table: "EmployeeExperiences",
                column: "ExperienceID");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeExperiences_Employee_EmployeeID",
                table: "EmployeeExperiences",
                column: "EmployeeID",
                principalTable: "Employee",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeExperiences_Experience_ExperienceID",
                table: "EmployeeExperiences",
                column: "ExperienceID",
                principalTable: "Experience",
                principalColumn: "ExperienceID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
