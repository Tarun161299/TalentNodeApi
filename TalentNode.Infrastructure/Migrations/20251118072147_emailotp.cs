using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TalentNode.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class emailotp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_EmailTemplates",
                table: "EmailTemplates");

            migrationBuilder.RenameTable(
                name: "EmailTemplates",
                newName: "EmailTemplate");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmailTemplate",
                table: "EmailTemplate",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_EmailTemplate",
                table: "EmailTemplate");

            migrationBuilder.RenameTable(
                name: "EmailTemplate",
                newName: "EmailTemplates");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmailTemplates",
                table: "EmailTemplates",
                column: "Id");
        }
    }
}
