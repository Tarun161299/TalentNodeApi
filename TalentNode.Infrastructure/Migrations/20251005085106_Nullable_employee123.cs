using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TalentNode.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Nullable_employee123 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeQualification_QualificationMaster_QualificationID",
                table: "EmployeeQualification");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeQualification_QualificationID",
                table: "EmployeeQualification");

            migrationBuilder.AddColumn<int>(
                name: "QualificationMasterQualificationID",
                table: "EmployeeQualification",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeQualification_QualificationMasterQualificationID",
                table: "EmployeeQualification",
                column: "QualificationMasterQualificationID");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeQualification_QualificationMaster_QualificationMasterQualificationID",
                table: "EmployeeQualification",
                column: "QualificationMasterQualificationID",
                principalTable: "QualificationMaster",
                principalColumn: "QualificationID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeQualification_QualificationMaster_QualificationMasterQualificationID",
                table: "EmployeeQualification");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeQualification_QualificationMasterQualificationID",
                table: "EmployeeQualification");

            migrationBuilder.DropColumn(
                name: "QualificationMasterQualificationID",
                table: "EmployeeQualification");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeQualification_QualificationID",
                table: "EmployeeQualification",
                column: "QualificationID");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeQualification_QualificationMaster_QualificationID",
                table: "EmployeeQualification",
                column: "QualificationID",
                principalTable: "QualificationMaster",
                principalColumn: "QualificationID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
